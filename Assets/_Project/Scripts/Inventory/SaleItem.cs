using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Inventory.Items;
using Assets._Project.Scripts.Player;
using Assets._Project.Scripts.ResourceExtraction.FishingRodMining;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Inventory.Items;
using Assets._Project.Sctipts.ResourceExtraction;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class SaleItem : MonoBehaviour
    {
        [SerializeField] private Button _sell;
        private Inventory _inventory;

        private Player _player;
        private Cell _currentCell;

        public void Initialize(Player player, Inventory inventor)
        {
            _player = player;
            _inventory = inventor;

            _sell.onClick.AddListener(Sele);
            _inventory.OnClickedItem += ChangeItem;
        }

        private void ChangeItem(Cell cell)
        {
            _currentCell = cell;
        }

        private void Sele()
        {
            if (_currentCell == null)
                return;

            if (_currentCell.Item.Category == ItemCategory.Weapon)
            {
                SaleWeapon();
            }
            else if(_currentCell.Item.Category == ItemCategory.Mining)
            {
                SaleMining();
            }
            else if (_currentCell.Item.Category == ItemCategory.Resource)
            {
                SaleResource();
            }
        }

        private void SaleWeapon()
        {
            if (_currentCell.NumberItems > 0)
            {
                _currentCell.SubtractNumberItems(1);
                WeaponItem weaponItem = _currentCell.Item as WeaponItem;
                if (weaponItem.TypeItem == WeaponConfigs.WoodenAxePlayerConfig.WeaponTypes)
                {
                    _player.Wallet.AddMoney(_currentCell.Item.Price);
                    if (_currentCell.NumberItems <= 0)
                    {
                        _currentCell.SetIsCellBusy(false);
                        Destroy(_currentCell.Item.gameObject);
                    }
                }
                else if (weaponItem.TypeItem == WeaponConfigs.WoodenSwordPlayerConfig.WeaponTypes)
                {
                    _player.Wallet.AddMoney(_currentCell.Item.Price);
                    if (_currentCell.NumberItems <= 0)
                    {
                        _currentCell.SetIsCellBusy(false);
                        Destroy(_currentCell.Item.gameObject);
                    }
                }
                else if (_currentCell.NumberItems <= 0)
                {
                    _currentCell.SetIsCellBusy(false);
                    Destroy(_currentCell.Item.gameObject);
                }
            }
        }

        private void SaleMining()
        {
            if (_currentCell.NumberItems > 0)
            {
                _currentCell.SubtractNumberItems(1);
                MiningItem weaponItem = _currentCell.Item as MiningItem;
                if (weaponItem.TypesMining == TypesMining.Pick)
                {
                    _player.Wallet.AddMoney(_currentCell.Item.Price);
                    if (_currentCell.NumberItems <= 0)
                    {
                        _currentCell.SetIsCellBusy(false);
                        Destroy(_currentCell.Item.gameObject);
                    }
                }
                else if(weaponItem.TypesMining == TypesMining.FishingRod)
                {
                    _player.Wallet.AddMoney(_currentCell.Item.Price);
                    if (_currentCell.NumberItems <= 0)
                    {
                        _currentCell.SetIsCellBusy(false);
                        Destroy(_currentCell.Item.gameObject);
                    }
                }
                else if (_currentCell.NumberItems <= 0)
                {
                    _currentCell.SetIsCellBusy(false);
                    Destroy(_currentCell.Item.gameObject);
                }
            }
        }

        private void SaleResource()
        {
            if (_currentCell.NumberItems > 0)
            {
                _currentCell.SubtractNumberItems(1);
                OreItem weaponItem = _currentCell.Item as OreItem;
                if (_currentCell.Item.GetItemType().Equals(ResourceExtraction.OreMining.TypesOre.Iron))
                {
                    _player.Wallet.AddMoney(_currentCell.Item.Price);
                    if (_currentCell.NumberItems <= 0)
                    {
                        _currentCell.SetIsCellBusy(false);
                        Destroy(_currentCell.Item.gameObject);
                    }
                }
                else if(_currentCell.Item.GetItemType().Equals(TypesFish.Carp))
                {
                    _player.Wallet.AddMoney(_currentCell.Item.Price);
                    if (_currentCell.NumberItems <= 0)
                    {
                        _currentCell.SetIsCellBusy(false);
                        Destroy(_currentCell.Item.gameObject);
                    }
                }
                else if (_currentCell.Item.GetItemType().Equals(TypesFish.Perch))
                {
                    _player.Wallet.AddMoney(_currentCell.Item.Price);
                    if (_currentCell.NumberItems <= 0)
                    {
                        _currentCell.SetIsCellBusy(false);
                        Destroy(_currentCell.Item.gameObject);
                    }
                }
                else if (_currentCell.NumberItems <= 0)
                {
                    _currentCell.SetIsCellBusy(false);
                    Destroy(_currentCell.Item.gameObject);
                }
            }
        }

        private void OnDestroy()
        {
            _sell.onClick.RemoveListener(Sele);
            _inventory.OnClickedItem -= ChangeItem;
        }
    }
}