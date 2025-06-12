using _Project.Core;
using _Project.Inventory.Items;
using _Project.Weapon;
using System;
using TMPro;
using UnityEngine;

namespace _Project.Inventory
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
                Enum weaponItem = cell.Item.GetItemType();
                ChangeTextForWeapon(weaponItem, cell);
            }
            else 
            {
                ChangeTextAllItem(cell);
            }
        }

        private void ChangeTextForWeapon(Enum weaponType, Cell cell)
        {
            if (weaponType.Equals(WeaponTypes.WoodenSwordPlayer))
                ChangeText(cell, WeaponConfigs.WoodenSwordPlayerConfig.VisibilityRadius, WeaponConfigs.WoodenSwordPlayerConfig.MinDamage,
                    WeaponConfigs.WoodenSwordPlayerConfig.MaxDamage);
            else if (weaponType.Equals(WeaponTypes.WoodenAxePlayer))
                ChangeText(cell, WeaponConfigs.WoodenAxePlayerConfig.VisibilityRadius, WeaponConfigs.WoodenAxePlayerConfig.MinDamage,
                    WeaponConfigs.WoodenAxePlayerConfig.MaxDamage);
            else if (weaponType.Equals(WeaponTypes.WoodenOnionPlayer))
                ChangeText(cell, WeaponConfigs.WeaponOnionPlayerConfig.VisibilityRadius, WeaponConfigs.WeaponOnionPlayerConfig.MinDamage,
                    WeaponConfigs.WeaponOnionPlayerConfig.MaxDamage);
        }

        private void ChangeText(Cell cell, float visibilityRadius, int minDamage, int maxDamage)
        {
            WeaponItem weaponItem = cell.Item as WeaponItem;
            _textInfoItem.text = $"{cell.Item.Name}\n " +
                $"Цена: {cell.Item.Price}\n " +
                $"Радиус атаки: {visibilityRadius}\n " +
                $"Урон: {minDamage}  -  {maxDamage} + прокачка: {weaponItem.ImprovementWeaponData.Damage}\n" +
                $"Уровень оружия: {weaponItem.Level}";
        }

        private void ChangeTextAllItem(Cell cell)
        {
            _textInfoItem.text = $"{cell.Item.Name}\n" +
                $"Цена: {cell.Item.Price}\n" +
                $"{cell.Item.Description}";
        }

        private void OnDestroy()
        {
            _sellItemsStore.OnClickItem -= Show;
        }
    }
}
