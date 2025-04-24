using Assets._Project.Scripts.Inventory;
using Assets._Project.Sctipts.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Cell _prefabCell;
        [SerializeField] private BaseItem _item;

        private List<Cell> _cellList = new();
        private RectTransform _contentInventory;

        public bool IsAddCell;
        public bool IsAddItem;

        public event Action<Cell> OnClickedItem;
        public event Action OnAddItem;

        public List<Cell> CellList => _cellList;

        public void Initialize(RectTransform contentInventory, List<Cell> cellList)
        {
            _contentInventory = contentInventory;
            _cellList = cellList;
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
            for (int i = 0; i < _cellList.Count; i++)
            {
                if (_cellList[i].IsCellBusy == false)
                {
                    BaseItem itemSpawn = Instantiate(item, _cellList[i].transform);
                    _cellList[i].Item = itemSpawn;
                    _cellList[i].SetIsCellBusy(true);
                    SetupItemButton(_cellList[i]);
                    UpdateCellInNumberItems(_cellList[i]);
                    break;
                }
                else if (_cellList[i].Item.Name == item.Name && _cellList[i].Item != null)
                {
                    UpdateCellInNumberItems(_cellList[i]);
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
            OnAddItem?.Invoke();
        }
    }
}