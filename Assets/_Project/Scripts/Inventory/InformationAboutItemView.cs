using _Project.Core;
using _Project.Weapon;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory
{
    public class InformationAboutItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textInfoItem;
        [SerializeField] private GameObject _itemDescriptionWindow;
        [SerializeField] private Button _exit;

        private Inventory _inventory;
        private InventoryActive _inventoryActive;
        private InventoryActivePotions _inventoryActivePotion;

        private Tween _tween;

        public void Initialize(Inventory inventory, InventoryActive inventoryActive, InventoryActivePotions inventoryActivePotion)
        {
            _inventory = inventory;
            _inventoryActive = inventoryActive;
            _inventoryActivePotion = inventoryActivePotion;

            _inventory.OnClickedItem += Show;
            _inventoryActive.OnClickedItem += Show;
            _inventoryActivePotion.OnClickedItem += Show;
            _exit.onClick.AddListener(Hide);
        }

        private void Show(Cell cell)
        {
            _itemDescriptionWindow.SetActive(true);
            _tween = _itemDescriptionWindow.transform
                .DOScale(new Vector3(0.6f, 1f, 1f), 0.5f);

            if (cell.Item.Category == ItemCategory.Weapon)
            {
                Enum weaponItem = cell.Item.GetItemType();
                ChangeTextForWeapon(weaponItem, cell);
            }
            else if (cell.Item.Category == ItemCategory.Mining || cell.Item.Category == ItemCategory.Resource || cell.Item.Category == ItemCategory.Potions)
            {
                ChangeTextForItem(cell);
            }
        }

        public void Hide()
        {
            _tween.Kill();

            _itemDescriptionWindow.SetActive(false);
            _itemDescriptionWindow.transform.localScale = new Vector3(0, 0, 0);
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

        private void ChangeTextForItem(Cell cell)
        {
            _textInfoItem.text = $"{cell.Item.Name}\n" +
                $"Цена: {cell.Item.Price}\n" +
                $"{cell.Item.Description}";
        }

        private void OnDestroy()
        {
            _inventory.OnClickedItem -= Show;
            _inventoryActive.OnClickedItem -= Show;
            _inventoryActivePotion.OnClickedItem -= Show;
            _exit.onClick.RemoveListener(Hide);
        }
    }
}
