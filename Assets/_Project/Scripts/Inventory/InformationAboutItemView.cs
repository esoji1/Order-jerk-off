using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using System;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Inventory
{
    public class InformationAboutItemView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _textInfoItem;

        private Sctipts.Inventory.Inventory _inventory;
        private InventoryActive _inventoryActive;

        public void Initialize(Sctipts.Inventory.Inventory inventory, InventoryActive inventoryActive)
        {
            _inventory = inventory;
            _inventoryActive = inventoryActive;
            _inventory.OnClickedItem += Show;
            _inventoryActive.OnClickedItem += Show;
        }

        private void Show(Cell cell)
        {
            if (cell.Item.Category == ItemCategory.Weapon)
            {
                Enum weaponItem = cell.Item.GetItemType();
                ChangeTextForWeapon(weaponItem, cell);
            }
            else if(cell.Item.Category == ItemCategory.Mining)
            {
                ChangeTextForMining(cell);
            }
            else if (cell.Item.Category == ItemCategory.Resource)
            {
                ChangeTextForResource(cell);
            }
        }

        private void ChangeTextForWeapon(Enum weaponType, Cell cell)
        {
            if (weaponType.Equals(WeaponTypes.WoodenSwordPlayer))
            {
                _textInfoItem.text = $"{cell.Item.Name}\n" +
                            $"Цена: {cell.Item.Price}\n" +
                            $"Радиус атаки: {WeaponConfigs.WoodenSwordPlayerConfig.VisibilityRadius}\n" +
                            $"Урон: {WeaponConfigs.WoodenSwordPlayerConfig.MinDamage}  -  {WeaponConfigs.WoodenSwordPlayerConfig.MaxDamage}";
            }
            else if (weaponType.Equals(WeaponTypes.WoodenAxePlayer))
            {
                _textInfoItem.text = $"{cell.Item.Name}\n" +
                           $"Цена: {cell.Item.Price}\n" +
                           $"Радиус атаки: {WeaponConfigs.WoodenAxePlayerConfig.VisibilityRadius}\n" +
                           $"Урон: {WeaponConfigs.WoodenAxePlayerConfig.MinDamage} -  {WeaponConfigs.WoodenSwordPlayerConfig.MaxDamage}";
            }
            else if (weaponType.Equals(WeaponTypes.WoodenOnionPlayer))
            {
                _textInfoItem.text = $"{cell.Item.Name}\n" +
                         $"Цена: {cell.Item.Price}\n" +
                         $"Радиус атаки: {WeaponConfigs.WeaponOnionPlayerConfig.VisibilityRadius}\n" +
                         $"Урон: {WeaponConfigs.WeaponOnionPlayerConfig.MinDamage} -  {WeaponConfigs.WeaponOnionPlayerConfig.MaxDamage}";
            }
        }

        private void ChangeTextForMining(Cell cell)
        {
            _textInfoItem.text = $"{cell.Item.Name}\n" +
                $"Цена: {cell.Item.Price}\n" +
                $"{cell.Item.Description}";
        }

        private void ChangeTextForResource(Cell cell)
        {
            _textInfoItem.text = $"{cell.Item.Name}\n" +
                $"Цена: {cell.Item.Price}\n" +
                $"{cell.Item.Description}";
        }

        private void OnDestroy()
        {
            _inventory.OnClickedItem -= Show;
            _inventoryActive.OnClickedItem -= Show;
        }
    }
}
