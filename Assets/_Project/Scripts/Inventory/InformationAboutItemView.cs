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

        private void Show(Item item)
        {
            if (item.Category == ItemCategory.Weapon)
            {
                if (item.WeaponType.ToString() == WeaponTypes.WoodenSwordPlayer.ToString())
                {
                    ChangeTextForWeapon(item.WeaponType);
                }
                else if (item.WeaponType.ToString() == WeaponTypes.WoodenAxePlayer.ToString())
                {
                    ChangeTextForWeapon(item.WeaponType);
                }
            }
        }

        private void ChangeTextForWeapon(WeaponType weaponType)
        {
            if (weaponType.ToString() == WeaponTypes.WoodenSwordPlayer.ToString())
            {
                _textInfoItem.text = $"Урон: {WeaponConfigs.WoodenSwordPlayerConfig.Damage}\n" +
                            $"Цена: {WeaponConfigs.WoodenSwordPlayerConfig.Price}\n" +
                            $"Радиус атаки: {WeaponConfigs.WoodenSwordPlayerConfig.VisibilityRadius}";
            }
            else if (weaponType.ToString() == WeaponTypes.WoodenAxePlayer.ToString())
            {
                _textInfoItem.text = $"Урон: {WeaponConfigs.WoodenAxePlayerConfig.Damage}\n" +
                           $"Цена: {WeaponConfigs.WoodenAxePlayerConfig.Price}\n" +
                           $"Радиус атаки: {WeaponConfigs.WoodenAxePlayerConfig.VisibilityRadius}";
            }
        }
    }
}
