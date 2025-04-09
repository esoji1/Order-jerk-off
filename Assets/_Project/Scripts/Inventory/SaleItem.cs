using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Player;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Inventory.Items;
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
        }

        private void OnDestroy()
        {
            _sell.onClick.RemoveListener(Sele);
            _inventory.OnClickedItem -= ChangeItem;
        }
    }
}