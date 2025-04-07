using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Inventory;
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
            if (cell.Item.Category == ItemCategory.Weapon)
            {
                if (cell.Item.WeaponType == WeaponTypes.WoodenSwordPlayer)
                {
                    ChangeTextForWeapon(cell.Item.WeaponType, cell);
                }
                else if (cell.Item.WeaponType == WeaponTypes.WoodenAxePlayer)
                {
                    ChangeTextForWeapon(cell.Item.WeaponType, cell);
                }
            }
        }

        private void ChangeTextForWeapon(WeaponTypes weaponType, Cell cell)
        {
            if (weaponType == WeaponTypes.WoodenSwordPlayer)
            {
                _textInfoItem.text = $"{WeaponConfigs.WoodenSwordPlayerConfig.NameWeapon}\n" +
                            $"Цена: {cell.Item.Price}\n" +
                            $"Радиус атаки: {WeaponConfigs.WoodenSwordPlayerConfig.VisibilityRadius}\n" +
                            $"Урон: {WeaponConfigs.WoodenSwordPlayerConfig.Damage}";
            }
            else if (weaponType == WeaponTypes.WoodenAxePlayer)
            {
                _textInfoItem.text = $"{WeaponConfigs.WoodenAxePlayerConfig.NameWeapon}\n" +
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
