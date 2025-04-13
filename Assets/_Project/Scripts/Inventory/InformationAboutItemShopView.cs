using Assets._Project.Scripts.Inventory.Items;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Inventory;
using Assets._Project.Sctipts.Inventory.Items;
using Assets._Project.Sctipts.ResourceExtraction;
using Assets._Project.Sctipts.ResourceExtraction.OreMining;
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
                WeaponItem weapon = cell.Item as WeaponItem;
                if (weapon.TypeItem == WeaponTypes.WoodenSwordPlayer)
                {
                    ChangeTextForWeapon(weapon.TypeItem, cell);
                }
                else if (weapon.TypeItem == WeaponTypes.WoodenAxePlayer)
                {
                    ChangeTextForWeapon(weapon.TypeItem, cell);
                }
            }
            else if (cell.Item.Category == ItemCategory.Mining)
            {
                MiningItem miningItem = cell.Item as MiningItem;
                if (miningItem.TypesMining == TypesMining.Pick)
                {
                    ChangeTextForMining(cell);
                }
            }
            else if (cell.Item.Category == ItemCategory.Resource)
            {
                OreItem miningItem = cell.Item as OreItem;
                if (miningItem.TypesOre == TypesOre.Iron)
                {
                    ChangeTextForResource(cell);
                }
            }
        }

        private void ChangeTextForWeapon(WeaponTypes weaponType, Cell cell)
        {
            if (weaponType == WeaponTypes.WoodenSwordPlayer)
            {
                _textInfoItem.text = $"{cell.Item.Name}\n" +
                            $"Цена: {cell.Item.Price}\n" +
                            $"Радиус атаки: {WeaponConfigs.WoodenSwordPlayerConfig.VisibilityRadius}\n" +
                            $"Урон: {WeaponConfigs.WoodenSwordPlayerConfig.Damage}";
            }
            else if (weaponType == WeaponTypes.WoodenAxePlayer)
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
            _sellItemsStore.OnClickItem -= Show;
        }
    }
}
