using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Inventory;
using Assets._Project.Sctipts.ResourceExtraction;
using System;
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
                Enum weaponItem = cell.Item.GetItemType();
                ChangeTextForWeapon(weaponItem, cell);
            }
            else if (cell.Item.Category == ItemCategory.Mining)
            {
                ChangeTextForMining(cell);
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
            _textInfoItem.text = $"{cell.Item.Name}\n " +
                $"Цена: {cell.Item.Price}\n " +
                $"Радиус атаки: {visibilityRadius}\n " +
                $"Урон: {minDamage}  -  {maxDamage}";
        }

        private void ChangeTextForMining(Cell cell)
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
