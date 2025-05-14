using _Project.Craft;
using _Project.Inventory;
using _Project.Inventory.ForgeInventory;
using _Project.Inventory.Items;
using Assets._Project.Scripts.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Improvements
{
    public class Improvements : MonoBehaviour
    {
        private const int MaxLevel = 3;

        [SerializeField] private Button _improveButton;
        [SerializeField] private Improve[] _improveData;

        [Header("Improvements")]
        [Header("Two level")]
        [SerializeField] private int _twoDamage;
        [SerializeField] private int _twoSpeedAttak;
        [Header("Three level")]
        [SerializeField] private int _threeDamage;
        [SerializeField] private int _threeSpeedAttak;

        private Inventory.Inventory _inventory;
        private InventoryForge _inventoryForge;

        private Improve _currentImprove;
        private Cell _clickitem;

        private void OnEnable() => _improveButton.onClick.AddListener(Craft);

        private void OnDisable() => _improveButton.onClick.RemoveListener(Craft);

        public void Initialize(Inventory.Inventory inventory, InventoryForge inventoryForge)
        {
            _inventory = inventory;
            _inventoryForge = inventoryForge;

            _inventoryForge.OnClickedItem += ChangeItemImprove;
        }

        private void ChangeItemImprove(Cell cell) => _clickitem = cell;

        private void Craft()
        {
            WeaponItem weaponItem = _clickitem.Item as WeaponItem;
            weaponItem.ImprovementWeaponData.Damage = 10;
            //if (HasAllIngredients())
            //{
            //    RemoveIngredients();
            //    Debug.Log($"Улучшение успешено!");
            //}
        }

        private bool HasAllIngredients()
        {
            int level = 0;

            foreach (Improve improve in _improveData)
            {
                if (improve.Item.GetItemType().Equals(_clickitem.Item.GetItemType()))
                {
                    WeaponItem weaponItem = _clickitem.Item as WeaponItem;

                    _currentImprove = improve;
                    level = weaponItem.Level;
                    break;
                }
            }

            if (level == 1)
            {
                bool canImprove = true;

                foreach (QuantityItemCraft requiredItem in _currentImprove.ImproveTwoLevel.List)
                {
                    int totalInInventory = 0;

                    foreach (Cell cell in _inventory.CellList)
                    {
                        if (cell.Item == null)
                            continue;

                        if (cell.Item.GetItemType().Equals(requiredItem.Item.GetItemType()))
                        {
                            totalInInventory += cell.NumberItems;
                            if (totalInInventory >= requiredItem.Quantity)
                                break;
                        }
                    }

                    if (totalInInventory < requiredItem.Quantity)
                    {
                        canImprove = false;
                        break;
                    }
                }

                if (canImprove)
                {
                    level++;
                    return true;
                }
            }

            Debug.Log("Нельзя улучшить оружие");

            return false;
        }
           

        private void RemoveIngredients()
        {
            //if (_currentImprove == null)
            //    return;

            //foreach (QuantityItemCraft requiredItem in _currentImprove.List)
            //{
            //    int remainingToRemove = requiredItem.Quantity;

            //    foreach (Cell cell in _inventory.CellList)
            //    {
            //        if (remainingToRemove <= 0) break;
            //        if (cell.Item == null) continue;

            //        if (requiredItem.Item.GetItemType().Equals(cell.Item.GetItemType()))
            //        {
            //            int canRemove = Mathf.Min(cell.NumberItems, remainingToRemove);
            //            _inventory.SubtractItems(cell, canRemove);
            //            remainingToRemove -= canRemove;
            //        }
            //    }
            //}

            //_inventory.AddItemInCell(_currentImprove.CraftItem);
            //_currentImprove = null;
        }

        private void OnDestroy()
        {
            _inventoryForge.OnClickedItem -= ChangeItemImprove;
        }
    }
}
