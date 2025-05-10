using _Project.Inventory.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory
{
    public class InventoryGrass : MonoBehaviour
    {
        private Cell[] _cellList = new Cell[5];
        private Inventory _inventory;

        private bool _isChangeValue;

        public event Action<Cell> OnClickedItem;

        public Cell[] CellList => _cellList;

        public void Initialize(Cell[] cellList, Inventory inventory)
        {
            _cellList = cellList;
            _inventory = inventory;

            _isChangeValue = true;

            _inventory.OnAddItem += AddItemInCell;
            _inventory.OnRemoveCell += RemoveItem;
            _inventory.OnSubstractItem += UpdateCountItem;

            AddItemInCell(_cellList[0]);
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

        public void AddItemInCell(BaseItem item, int itemCount)
        {
            for (int i = 0; i < _cellList.Length; i++)
            {
                if (_cellList[i].Item.Name == item.Name && _cellList[i].Item != null)
                {
                    _cellList[i].AddNumberItems(itemCount);
                    break;
                }
            }
        }

        public bool SubtractItems(Cell cell, int numberSubtract)
        {
            cell.SubtractNumberItems(numberSubtract);

            if (cell.NumberItems <= 0)
            {
                cell.NumberItems = 0;
                return false;
            }

            return true;
        }

        public void SetChangeValue(bool value) => _isChangeValue = value;

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
            if (_isChangeValue == false)
                return;

            for (int i = 0; i < _cellList.Length; i++)
            {
                if (_cellList[i].Item != null && cell.Item.GetItemType().Equals(_cellList[i].Item.GetItemType()))
                {
                    _cellList[i].NumberItems = cell.NumberItems;
                    _cellList[i].AddNumberItems(0);
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
            _inventory.OnAddItem -= AddItemInCell;
            _inventory.OnRemoveCell -= RemoveItem;
            _inventory.OnSubstractItem -= UpdateCountItem;
        }
    }
}