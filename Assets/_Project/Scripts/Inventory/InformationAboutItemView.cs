using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Inventory
{
    public class InformationAboutItemView : MonoBehaviour
    {
        [SerializeField] private Sctipts.Inventory.Inventory _inventory;
        [SerializeField] TextMeshProUGUI _textInfoItem;

        private void OnEnable()
        {
            _inventory.OnClickedItem += Show;
        }

        private void OnDisable()
        {
            _inventory.OnClickedItem -= Show;
        }

        private void Show(Cell cell)
        {
            if (cell.Item.Category == ItemCategory.Weapon)
            {
                if (cell.Item.WeaponType == WeaponTypes.WoodenSwordPlayer)
                {
                    ChangeTextForWeapon(cell.Item.WeaponType);
                }
                else if (cell.Item.WeaponType == WeaponTypes.WoodenAxePlayer)
                {
                    ChangeTextForWeapon(cell.Item.WeaponType);
                }
            }
        }

        private void ChangeTextForWeapon(WeaponTypes weaponType)
        {
            if (weaponType == WeaponTypes.WoodenSwordPlayer)
            {
                _textInfoItem.text = $"{WeaponConfigs.WoodenSwordPlayerConfig.NameWeapon}\n" +
                            $"Цена: {WeaponConfigs.WoodenSwordPlayerConfig.Price}\n" +
                            $"Радиус атаки: {WeaponConfigs.WoodenSwordPlayerConfig.VisibilityRadius}\n" +
                            $"Урон: {WeaponConfigs.WoodenSwordPlayerConfig.Damage}";
            }
            else if (weaponType == WeaponTypes.WoodenAxePlayer)
            {
                _textInfoItem.text = $"{WeaponConfigs.WoodenAxePlayerConfig.NameWeapon}\n" +
                           $"Цена: {WeaponConfigs.WoodenAxePlayerConfig.Price}\n" +
                           $"Радиус атаки: {WeaponConfigs.WoodenAxePlayerConfig.VisibilityRadius}\n" +
                           $"Урон: {WeaponConfigs.WoodenAxePlayerConfig.Damage}";
            }
        }
    }
}
