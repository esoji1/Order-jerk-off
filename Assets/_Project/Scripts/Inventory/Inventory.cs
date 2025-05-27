using _Project.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Cell _prefabCell;
        [SerializeField] private BaseItem[] _item;

        private List<Cell> _cellList = new();
        private RectTransform _contentInventory;

        public bool IsAddCell;
        public bool IsAddItem;

        public event Action<Cell> OnClickedItem;
        public event Action<Cell> OnAddItem;
        public event Action<Cell> OnSubstractItem;
        public event Action<BaseItem> OnRemoveCell;

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
                for (int i = 0; i < _item.Length; i++)
                {
                    AddItemInCell(_item[i]);
                }
                IsAddItem = false;
            }
        }

        public void AddItemInCell(BaseItem item)
        {
            for (int i = 0; i < _cellList.Count; i++)
            {
                if ((item as WeaponItem) == false)
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
                else
                {
                    if (_cellList[i].IsCellBusy)
                    {
                        continue;
                    }
                    else if (_cellList[i].IsCellBusy == false)
                    {
                        BaseItem itemSpawn = Instantiate(item, _cellList[i].transform);
                        _cellList[i].Item = itemSpawn;
                        _cellList[i].SetIsCellBusy(true);
                        SetupItemButton(_cellList[i]);
                        UpdateCellInNumberItems(_cellList[i]);
                        break;
                    }
                }
            }
        }

        public void MoveItemToCell(BaseItem item, Cell cell)
        {
            for (int i = 0; i < _cellList.Count; i++)
            {
                if (_cellList[i].IsCellBusy)
                {
                    continue;
                }
                else if (_cellList[i].IsCellBusy == false && item != null)
                {
                    item.transform.SetParent(_cellList[i].transform);
                    item.transform.localPosition = new Vector3(0f, 0f, 0f);
                    item.transform.localScale = new Vector3(1f, 1f, 1f);
                    _cellList[i].Item = item;
                    _cellList[i].SetIsCellBusy(true);
                    SetupItemButton(_cellList[i]);
                    _cellList[i].AddNumberItems(1);
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
            OnSubstractItem?.Invoke(cell);

            if (cell.NumberItems <= 0)
            {
                OnRemoveCell?.Invoke(cell.Item);
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
            OnAddItem?.Invoke(cell);
        }
    }
}