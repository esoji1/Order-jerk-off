using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Inventory;
using Assets._Project.Sctipts.Inventory.Items;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Inventory
{
    public class InformationAboutItemShopView : MonoBehaviour 
    {
        [SerializeField] TextMeshProUGUI _textInfoItem;

        private SellItemsStore _sellItemsStore;

        public void Initialize(SellItemsStore sellItemsStore)
        {
            _sellItemsStore = sellItemsStore;
            _sellItemsStore.OnClickItem += Show;
        }

        private void Show(Cell cell)
        {
            WeaponItem weapon = cell.Item as WeaponItem;
            if (cell.Item.Category == ItemCategory.Weapon)
            {
                if (weapon.TypeItem == WeaponTypes.WoodenSwordPlayer)
                {
                    ChangeTextForWeapon(weapon.TypeItem, cell);
                }
                else if (weapon.TypeItem == WeaponTypes.WoodenAxePlayer)
                {
                    ChangeTextForWeapon(weapon.TypeItem, cell);
                }
            }
        }

        private void ChangeTextForWeapon(WeaponTypes weaponType, Cell cell)
        {
            if (weaponType == WeaponTypes.WoodenSwordPlayer)
            {
                _textInfoItem.text = $"{cell.Item.Name}\n" +
                            $"Цена: {cell.Item.Price}\n" +
                            $"Радиус атаки: {WeaponConfigs.WoodenSwordPlayerConfig.VisibilityRadius}\n" +
                            $"Урон: {WeaponConfigs.WoodenSwordPlayerConfig.Damage}";
            }
            else if (weaponType == WeaponTypes.WoodenAxePlayer)
            {
                _textInfoItem.text = $"{cell.Item.Name}\n" +
                           $"Цена: {cell.Item.Price}\n" +
                           $"Радиус атаки: {WeaponConfigs.WoodenAxePlayerConfig.VisibilityRadius}\n" +
                           $"Урон: {WeaponConfigs.WoodenAxePlayerConfig.Damage}";
            }
        }

        private void OnDestroy()
        {
            _sellItemsStore.OnClickItem -= Show;
        }
    }
}
