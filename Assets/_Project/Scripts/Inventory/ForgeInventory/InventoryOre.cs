using _Project.Inventory.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory.ForgeInventory
{
    public class InventoryOre : MonoBehaviour
    {
        private Cell[] _cellList = new Cell[5];
        private Inventory _inventory;

        public event Action<Cell> OnClickedItem;

        public Cell[] CellList => _cellList;

        public void Initialize(Cell[] cellList, Inventory inventory)
        {
            _cellList = cellList;
            _inventory = inventory;

            _inventory.OnAddItem += AddItemInCell;
            _inventory.OnRemoveCell += RemoveItem;
            _inventory.OnSubstractItem += UpdateCountItem;

            UpdateItem();
        }

        public void AddItemInCell(Cell cell)
        {
            for (int i = 0; i < _cellList.Length; i++)
            {
                if (_cellList[i].Item != null)
                {
                    if (cell.Item.GetItemType().Equals(_cellList[i].Item.GetItemType()))
                    {
                        _cellList[i].NumberItems = cell.NumberItems;
                        _cellList[i].AddNumberItems(0);
                        _cellList[i].SetIsCellBusy(true);
                        SetupItemButton(_cellList[i]);
                    }
                }
            }
        }

        private void RemoveItem(BaseItem item)
        {
            for (int i = 0; i < _cellList.Length; i++)
            {
                if (_cellList[i].Item != null)
                {
                    if (_cellList[i].Item.GetItemType().Equals(item.GetItemType()))
                    {
                        _cellList[i].NumberItems = 0;
                    }
                }
            }
        }

        private void UpdateCountItem(Cell cell)
        {
            for (int i = 0; i < _cellList.Length; i++)
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
                if (_inventory.CellList[i].Item != null)
                {
                    for (int j = 0; j < _cellList.Length; j++)
                    {
                        if (_cellList[j].Item != null)
                        {
                            if (_cellList[j].Item.GetItemType().Equals(_inventory.CellList[i].Item.GetItemType()))
                            {
                                _cellList[j].NumberItems = _inventory.CellList[i].NumberItems;
                                _cellList[j].AddNumberItems(0);
                                _cellList[j].SetIsCellBusy(true);
                                SetupItemButton(_cellList[j]);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void SetupItemButton(Cell cell)
        {
            if (cell.Item != null)
            {
                if (cell.Item.TryGetComponent(out Button button))
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => OnItemClicked(cell));
                }
            }
        }

        private void OnItemClicked(Cell cell)
        {
            OnClickedItem?.Invoke(cell);
        }

        private void OnDestroy()
        {
            if (_inventory != null)
            {
                _inventory.OnAddItem -= AddItemInCell;
                _inventory.OnRemoveCell -= RemoveItem;
                _inventory.OnSubstractItem -= UpdateCountItem;
            }
        }
    }
}