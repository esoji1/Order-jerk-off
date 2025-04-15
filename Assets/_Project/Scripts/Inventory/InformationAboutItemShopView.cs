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
            {
                _textInfoItem.text = $"{cell.Item.Name}\n" +
                            $"Цена: {cell.Item.Price}\n" +
                            $"Радиус атаки: {WeaponConfigs.WoodenSwordPlayerConfig.VisibilityRadius}\n" +
                            $"Урон: {WeaponConfigs.WoodenSwordPlayerConfig.MinDamage} -  {WeaponConfigs.WoodenSwordPlayerConfig.MaxDamage}";
            }
            else if (weaponType.Equals(WeaponTypes.WoodenAxePlayer))
            {
                _textInfoItem.text = $"{cell.Item.Name}\n" +
                           $"Цена: {cell.Item.Price}\n" +
                           $"Радиус атаки: {WeaponConfigs.WoodenAxePlayerConfig.VisibilityRadius}\n" +
                           $"Урон: {WeaponConfigs.WoodenSwordPlayerConfig.MinDamage} -  {WeaponConfigs.WoodenSwordPlayerConfig.MaxDamage}";
            }
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
