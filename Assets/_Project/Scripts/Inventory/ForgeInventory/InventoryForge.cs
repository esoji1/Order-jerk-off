using _Project.Inventory.Items;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory.ForgeInventory
{
    public class InventoryForge : MonoBehaviour
    {
        [SerializeField] private Cell _prefabCell;
        [SerializeField] private BaseItem _item;

        private List<Cell> _cellList = new();
        private RectTransform _contentInventory;
        private Inventory _inventory;

        public bool IsAddCell;
        public bool IsAddItem;

        public event Action<Cell> OnClickedItem;

        public List<Cell> CellList => _cellList;

        public void Initialize(RectTransform contentInventory, List<Cell> cellList, Inventory inventory)
        {
            _contentInventory = contentInventory;
            _cellList = cellList;
            _inventory = inventory;

            _inventory.OnAddItem += AddItemInCell;
            _inventory.OnSubstractItem += UpdateCountItem;
            _inventory.OnRemoveCell += RemoveItem;
            UpdateItem();
        }

        public void AddItemInCell(Cell cell)
        {
            if (cell.Item.Category != ItemCategory.Weapon)
                return;

            for (int i = 0; i < _cellList.Count; i++)
            {
                if (_cellList[i].IsCellBusy == false)
                {
                    BaseItem itemSpawn = Instantiate(cell.Item, _cellList[i].transform);
                    _cellList[i].Item = itemSpawn;
                    _cellList[i].SetIsCellBusy(true);
                    SetupItemButton(_cellList[i]);
                    UpdateCellInNumberItems(_cellList[i]);
                    break;
                }
                else if (_cellList[i].Item.Name == cell.Item.Name && _cellList[i].Item != null)
                {
                    UpdateCountItem(cell);
                    break;
                }
            }
        }

        public bool SubtractItems(Cell cell, int numberSubtract)
        {
            cell.SubtractNumberItems(numberSubtract);

            if (cell.NumberItems <= 0)
            {
                cell.SetIsCellBusy(false);
                Destroy(cell.Item.gameObject);
                cell.Item = null;
                return false;
            }

            return true;
        }

        private void UpdateCountItem(Cell cell)
        {
            for (int i = 0; i < _cellList.Count; i++)
            {
                if (_cellList[i].Item != null && cell.Item.GetItemType().Equals(_cellList[i].Item.GetItemType()))
                {
                    _cellList[i].NumberItems = cell.NumberItems;
                    _cellList[i].AddNumberItems(0);
                }
            }
        }

        private void UpdateItem()
        {
            int index = 0;

            for (int i = 0; i < _inventory.CellList.Count; i++)
            {
                if (_inventory.CellList[i].Item != null && _inventory.CellList[i].Item.Category == ItemCategory.Weapon)
                {
                    if (_cellList[index].IsCellBusy == false)
                    {
                        BaseItem itemSpawn = Instantiate(_inventory.CellList[i].Item, _cellList[index].transform);
                        _cellList[index].Item = itemSpawn;
                        _cellList[index].SetIsCellBusy(true);
                        SetupItemButton(_cellList[index]);
                        _cellList[index].NumberItems = _inventory.CellList[i].NumberItems;
                        _cellList[index].AddNumberItems(0);
                        index++;
                    }
                }
            }
        }

        private void RemoveItem(BaseItem item)
        {
            for (int i = 0; i < _cellList.Count; i++)
            {
                if (_cellList[i].Item != null)
                {
                    if (_cellList[i].Item.GetItemType().Equals(item.GetItemType()))
                    {
                        _cellList[i].NumberItems = 0;
                        _cellList[i].SetIsCellBusy(false);
                        Destroy(_cellList[i].Item.gameObject);
                        _cellList[i].Item = null;
                    }
                }
            }
        }

        private void SetupItemButton(Cell cell)
        {
            if (cell.Item.TryGetComponent(out Button button))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnItemClicked(cell));
            }
        }

        private void OnItemClicked(Cell cell)
        {
            OnClickedItem?.Invoke(cell);
        }

        private void AddCell(int numberCells)
        {
            for (int i = 0; i < numberCells; i++)
            {
                Cell cell = Instantiate(_prefabCell, _contentInventory.transform);
                _cellList.Add(cell);
            }
        }

        private void AddCellFilling(int numberLines)
        {
            for (int i = 0; i < numberLines; i++)
                AddCell(8);
        }

        private void UpdateCellInNumberItems(Cell cell)
        {
            cell.AddNumberItems(1);
        }

        private void OnDestroy()
        {
            _inventory.OnAddItem -= AddItemInCell;
            _inventory.OnSubstractItem -= UpdateCountItem;
            _inventory.OnRemoveCell -= RemoveItem;
        }
    }
}
