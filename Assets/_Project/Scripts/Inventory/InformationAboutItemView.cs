using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Inventory.Items;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Inventory
{
    public class InformationAboutItemView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _textInfoItem;

        private Sctipts.Inventory.Inventory _inventory;

        public void Initialize(Sctipts.Inventory.Inventory inventory)
        {
            _inventory = inventory;
            _inventory.OnClickedItem += Show;
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
            _inventory.OnClickedItem -= Show;
        }
    }
}
