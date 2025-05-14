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

            UpdateItem();
        }

        public void MoveItemToCell(BaseItem item, Cell cell)
        {
            for (int i = 0; i < _cellList.Count; i++)
            {
                if (_cellList[i].IsCellBusy)
                {
                    continue;
                }
                else if (_cellList[i].IsCellBusy == false && item != null && item.Category == ItemCategory.Weapon)
                {
                    item.transform.SetParent(_cellList[i].transform);
                    item.transform.localPosition = new Vector3(0f, 0f, 0f);
                    item.transform.localScale = new Vector3(1f, 1f, 1f);
                    _cellList[i].Item = item;
                    _cellList[i].SetIsCellBusy(true);
                    SetupItemButton(_cellList[i]);
                    UpdateCellInNumberItems(_cellList[i]);
                    cell.SetIsCellBusy(false);
                    cell.Item = null;
                    cell.NumberItems = 0;
                    cell.AddNumberItems(0);
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
            for (int i = 0; i < _inventory.CellList.Count; i++)
            {
                MoveItemToCell(_inventory.CellList[i].Item, _inventory.CellList[i]);
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
    }
}
