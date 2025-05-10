using _Project.Inventory.Items;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory.AlchemyInventory
{
    public class InventoryActiveGrass : MonoBehaviour
    {
        private Cell[] _cell = new Cell[2];

        public event Action<Cell> OnClickedItem;

        public Cell[] Cell => _cell;

        public void Initialize(Cell[] cellWeapon)
        {
            _cell = cellWeapon;
        }

        public void AddItemInCell(BaseItem item)
        {
            for (int i = 0; i < _cell.Length; i++)
            {
                if (_cell[i].IsCellBusy == false)
                {
                    BaseItem itemSpawn = Instantiate(item, _cell[i].transform);
                    _cell[i].Item = itemSpawn;
                    _cell[i].SetIsCellBusy(true);
                    SetupItemButton(_cell[i]);
                    UpdateCellInNumberItems(_cell[i]);
                    break;
                }
                else if (_cell[i].Item.Name == item.Name && _cell[i].Item != null)
                {
                    UpdateCellInNumberItems(_cell[i]);
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

        private void UpdateCellInNumberItems(Cell cell)
        {
            cell.AddNumberItems(1);
        }
    }
}