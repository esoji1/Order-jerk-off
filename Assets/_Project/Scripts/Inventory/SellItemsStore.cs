using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Inventory.Items;
using Assets._Project.Scripts.Player;
using Assets._Project.Scripts.ScriptableObjects.Configs;
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
            if (_inventory.CellList.Count <= 0 || _currentCell == null)
                return;

            if (_currentCell.Item.Category == ItemCategory.Weapon)
            {
                if (_player.Wallet.SubtractMoney(_currentCell.Item.Price))
                {
                    WeaponItem weaponItem = _currentCell.Item as WeaponItem;

                    foreach (WeaponItem item in _itemData.WeaponItems)
                        if (weaponItem.TypeItem == item.TypeItem)
                            _inventory.AddItemInCell(item);
                }
            }
            else if (_currentCell.Item.Category == ItemCategory.Mining)
            {
                if (_player.Wallet.SubtractMoney(_currentCell.Item.Price))
                {
                    MiningItem miningItem = _currentCell.Item as MiningItem;

                    foreach (MiningItem item in _itemData.MiningItems)
                        if (miningItem.TypesMining == item.TypesMining)
                            _inventory.AddItemInCell(item);
                }
            }
        }

        private void OnDestroy() => 
            _buy.onClick.RemoveListener(Buy);
    }
}