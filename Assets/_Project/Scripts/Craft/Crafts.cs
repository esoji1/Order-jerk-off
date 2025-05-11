using _Project.Inventory;
using _Project.Inventory.AlchemyInventory;
using Assets._Project.Scripts.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Craft
{
    public class Crafts : MonoBehaviour
    {
        [SerializeField] private Button _craftButton;
        [SerializeField] private CraftData craftData;

        private InventoryActiveGrass _inventoryActiveGrass;
        private Inventory.Inventory _inventory;

        private Craft _currentCraft;

        private void OnEnable() => _craftButton.onClick.AddListener(Craft);

        private void OnDisable() => _craftButton.onClick.RemoveListener(Craft);

        public void Initialize(InventoryActiveGrass inventoryActiveGrass, Inventory.Inventory inventory)
        {
            _inventoryActiveGrass = inventoryActiveGrass;
            _inventory = inventory;
        }

        private void Craft()
        {
            if (HasAllIngredients())
            {
                RemoveIngredients();
                Debug.Log($"Крафт успешен!");
            }
        }

        private bool HasAllIngredients()
        {
            foreach (Craft craft in craftData.Crafts)
            {
                bool canCraft = true;

                foreach (QuantityItemCraft requiredItem in craft.List)
                {
                    int totalInInventory = 0;

                    foreach (Cell cell in _inventoryActiveGrass.Cell)
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
                        canCraft = false;
                        break;
                    }
                }

                if (canCraft)
                {
                    _currentCraft = craft;
                    return true;
                }
            }

            Debug.Log("Недостаточно предметов для любого крафта");
            return false;
        }

        private void RemoveIngredients()
        {
            if (_currentCraft == null)
            {
                Debug.Log("Нет активного крафта!");
                return;
            }

            foreach (QuantityItemCraft requiredItem in _currentCraft.List)
            {
                int remainingToRemove = requiredItem.Quantity;

                foreach (Cell cell in _inventoryActiveGrass.Cell)
                {
                    if (remainingToRemove <= 0) break;
                    if (cell.Item == null) continue;

                    if (requiredItem.Item.GetItemType().Equals(cell.Item.GetItemType()))
                    {
                        int canRemove = Mathf.Min(cell.NumberItems, remainingToRemove);

                        RemoveFromMainInventory(cell.Item.GetItemType(), canRemove);
                        _inventoryActiveGrass.SubtractItems(cell, canRemove);

                        remainingToRemove -= canRemove;
                    }
                }
            }

            _inventory.AddItemInCell(_currentCraft.CraftItem);
            _currentCraft = null;
        }

        private void RemoveFromMainInventory(Enum itemType, int count)
        {
            foreach (Cell cell in _inventory.CellList)
            {
                if (count <= 0) break;
                if (cell.Item == null) continue;

                if (itemType.Equals(cell.Item.GetItemType()))
                {
                    int canRemove = Mathf.Min(cell.NumberItems, count);
                    _inventory.SubtractItems(cell, canRemove);
                    count -= canRemove;
                }
            }
        }
    }
}