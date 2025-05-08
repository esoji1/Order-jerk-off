using _Project.Inventory.Items;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory
{
    public class InventoryActivePotions : MonoBehaviour
    {
        [SerializeField] private Cell _prefabCell;
        [SerializeField] private BaseItem _item;

        private List<Cell> _cellPotionsList = new();
        private RectTransform _contentInventory;
        private bool _isOpen;

        public bool IsAddCell;
        public bool IsAddItem;

        public event Action<Cell> OnClickedItem;
        public event Action OnAddItem;
        public event Action<Enum> OnSubstartcItem;

        public List<Cell> CellList => _cellPotionsList;
        public bool IsOpen => _isOpen;

        public void Initialize(RectTransform contentInventory, List<Cell> cellPotionsList)
        {
            _contentInventory = contentInventory;
            _cellPotionsList = cellPotionsList;
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
            for (int i = 0; i < _cellPotionsList.Count; i++)
            {
                if (_cellPotionsList[i].IsCellBusy == false)
                {
                    BaseItem itemSpawn = Instantiate(item, _cellPotionsList[i].transform);
                    _cellPotionsList[i].Item = itemSpawn;
                    _cellPotionsList[i].SetIsCellBusy(true);
                    SetupItemButton(_cellPotionsList[i]);
                    UpdateCellInNumberItems(_cellPotionsList[i]);
                    break;
                }
                else if (_cellPotionsList[i].Item.Name == item.Name && _cellPotionsList[i].Item != null)
                {
                    UpdateCellInNumberItems(_cellPotionsList[i]);
                    break;
                }
            }
        }

        public bool SubtractItems(Cell cell, int numberSubtract)
        {
            cell.SubtractNumberItems(numberSubtract);
            OnSubstartcItem?.Invoke(cell.Item.GetItemType());
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
                _cellPotionsList.Add(cell);
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
            OnAddItem?.Invoke();
        }
    }
}