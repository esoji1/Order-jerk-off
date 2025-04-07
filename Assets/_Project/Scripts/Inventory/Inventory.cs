using Assets._Project.Scripts.Inventory;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private RectTransform _contentInventory;
        [SerializeField] private Cell _prefabCell;
        [SerializeField] private Item _item;

        private List<Cell> _cellList = new();

        public bool IsAddCell;
        public bool IsAddItem;

        public event Action<Cell> OnClickedItem;

        public List<Cell> CellList => _cellList;

        private void Initialize()
        {
            InitializeCellFilling(6);
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

        public void AddItemInCell(Item item)
        {
            for (int i = 0; i < _cellList.Count; i++)
            {
                if (_cellList[i].IsCellBusy == false)
                {
                    Item itemSpawn = Instantiate(item, _cellList[i].transform);
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

        private void InitializeCellFilling(int numberLines)
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