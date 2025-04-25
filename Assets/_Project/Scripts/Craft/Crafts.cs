using Assets._Project.Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Craft
{
    public class Crafts : MonoBehaviour
    {
        [SerializeField] private Button _craftButton;

        private InventoryCrafting _inventoryCrafting;

        private Sctipts.Craft.Craft _currentCraft;

        public void Initialize(InventoryCrafting inventoryCrafting)
        {
            _inventoryCrafting = inventoryCrafting;

            _inventoryCrafting.OnClickCraft += ChangeCraftItem;
            _craftButton.onClick.AddListener(Craft);
        }

        private void Craft()
        {
            if (_currentCraft == null)
                return;

            if (HasAllIngredients() == false)
            {
                Debug.Log("Not enough ingredients for crafting");
                return;
            }

            RemoveIngredients();

            _inventoryCrafting.Inventory.AddItemInCell(_currentCraft.CraftItem);
        }

        private bool HasAllIngredients()
        {
            foreach (QuantityItemCraft item in _currentCraft.List)
            {
                int totalQuantity = 0;

                foreach (Cell cell in _inventoryCrafting.Inventory.CellList)
                {
                    if (cell.Item == null) continue;

                    if (item.Item.GetItemType().Equals(cell.Item.GetItemType()))
                    {
                        totalQuantity += cell.NumberItems;
                        if (totalQuantity >= item.Quantity)
                            break;
                    }
                }

                if (totalQuantity < item.Quantity)
                    return false;
            }

            return true;
        }

        private void RemoveIngredients()
        {
            foreach (QuantityItemCraft item in _currentCraft.List)
            {
                int remainingToRemove = item.Quantity;

                foreach (Cell cell in _inventoryCrafting.Inventory.CellList)
                {
                    if (cell.Item == null) continue;

                    if (item.Item.GetItemType().Equals(cell.Item.GetItemType()))
                    {
                        int canRemove = Mathf.Min(cell.NumberItems, remainingToRemove);
                        cell.SubtractNumberItems(canRemove);
                        remainingToRemove -= canRemove;

                        if (cell.NumberItems <= 0)
                        {
                            cell.SetIsCellBusy(false);
                            Destroy(cell.Item.gameObject);
                            cell.Item = null;
                        }

                        if (remainingToRemove <= 0)
                            break;
                    }
                }
            }
        }

        private void ChangeCraftItem(Sctipts.Craft.Craft craft) => _currentCraft = craft;

        private void OnDestroy()
        {
            _inventoryCrafting.OnClickCraft -= ChangeCraftItem;
            _craftButton.onClick.RemoveListener(Craft);
        }
    }
}