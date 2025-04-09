using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Player;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class SellItemsStore : MonoBehaviour
    {
        [SerializeField] private Button _buy;
        [SerializeField] private List<Cell> _cellList;
        [SerializeField] private ItemData _itemData;

        private Player _player;
        private Inventory _inventory;

        private Cell _currentCell;

        public event Action<Cell> OnClickItem;

        public void Initialize(Player player, Inventory inventory)
        {
            _player = player;
            _inventory = inventory;

            foreach (Cell cell in _cellList)
            {
                if (cell.Item.TryGetComponent(out Button button))
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => OnItemClicked(cell));
                }
            }

            _buy.onClick.AddListener(Buy);
        }

        private void OnItemClicked(Cell cell)
        {
            _currentCell = cell;
            OnClickItem?.Invoke(_currentCell);
        }

        private void Buy()
        {
            if (_inventory.CellList.Count <= 0)
                return;
            if (_currentCell.Item.Category == ItemCategory.Weapon)
            {
                if (_player.Wallet.SubtractMoney(_currentCell.Item.Price))
                {
                    WeaponItem weaponItem = _currentCell.Item as WeaponItem;

                    if (weaponItem.TypeItem == WeaponTypes.WoodenSwordPlayer)
                    {
                        _inventory.AddItemInCell(_itemData.WoodenSwordItem);
                    }
                    else if (weaponItem.TypeItem == WeaponTypes.WoodenAxePlayer)
                    {
                        _inventory.AddItemInCell(_itemData.WoodenAxeItem);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _buy.onClick.RemoveListener(Buy);
        }
    }
}