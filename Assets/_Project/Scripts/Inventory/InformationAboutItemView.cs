using Assets._Project.Scripts.ResourceExtraction.FishingRodMining;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.ResourceExtraction;
using Assets._Project.Sctipts.ResourceExtraction.OreMining;
using System;
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
                            $"Урон: {WeaponConfigs.WoodenSwordPlayerConfig.Damage}";
            }
            else if (weaponType.Equals(WeaponTypes.WoodenAxePlayer))
            {
                _textInfoItem.text = $"{cell.Item.Name}\n" +
                           $"Цена: {cell.Item.Price}\n" +
                           $"Радиус атаки: {WeaponConfigs.WoodenAxePlayerConfig.VisibilityRadius}\n" +
                           $"Урон: {WeaponConfigs.WoodenAxePlayerConfig.Damage}";
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
        }
    }
}
