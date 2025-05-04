using _Project.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory
{
    public class InventoryActive : MonoBehaviour
    {
        [SerializeField] private Cell _prefabCell;
        [SerializeField] private BaseItem _item;

        private List<Cell> _cellWeaponList = new();
        private RectTransform _contentInventory;
        private bool _isOpen;

        public bool IsAddCell;
        public bool IsAddItem;

        public event Action<Cell> OnClickedItem;

        public List<Cell> CellList => _cellWeaponList;
        public bool IsOpen => _isOpen;

        public void Initialize(RectTransform contentInventory, List<Cell> cellWeaponList)
        {
            _contentInventory = contentInventory;
            _cellWeaponList = cellWeaponList;
            _isOpen = true;
        }

        private void Update()
        {
            if (IsAddCell)
            {
                AddCell(8);
                IsAddCell = false;
            }
            else if (IsAddItem)
            {
                AddItemInCell(_item);
                IsAddItem = false;
            }
        }

        public void AddItemInCell(BaseItem item)
        {
            for (int i = 0; i < _cellWeaponList.Count; i++)
            {
                if (_cellWeaponList[i].IsCellBusy == false)
                {
                    BaseItem itemSpawn = Instantiate(item, _cellWeaponList[i].transform);
                    _cellWeaponList[i].Item = itemSpawn;
                    _cellWeaponList[i].SetIsCellBusy(true);
                    SetupItemButton(_cellWeaponList[i]);
                    UpdateCellInNumberItems(_cellWeaponList[i]);
                    break;
                }
                else if (_cellWeaponList[i].Item.Name == item.Name && _cellWeaponList[i].Item != null)
                {
                    UpdateCellInNumberItems(_cellWeaponList[i]);
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
                _cellWeaponList.Add(cell);
            }
        }

        private void AddCellFilling(int numberLines)
        {
            for (int i = 0; i < numberLines; i++)
                AddCell(3);
        }

        private void UpdateCellInNumberItems(Cell cell)
        {
            cell.AddNumberItems(1);
        }
    }
}