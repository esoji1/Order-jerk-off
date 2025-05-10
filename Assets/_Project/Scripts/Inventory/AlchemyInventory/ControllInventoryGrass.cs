using UnityEngine;

namespace _Project.Inventory.AlchemyInventory
{
    public class ControllInventoryGrass : MonoBehaviour
    {
        private InventoryGrass _inventoryGrass;
        private InventoryActiveGrass _inventoryActiveGrass;
        private GameObject _window;

        private void Update()
        {
            if (_window != null)
            {
                if (_window.activeSelf == false)
                {
                    foreach (Cell cell in _inventoryActiveGrass.Cell)
                    {
                        if (cell.Item != null)
                        {
                            _inventoryGrass.AddItemInCell(cell.Item, cell.NumberItems);
                            _inventoryActiveGrass.SubtractItems(cell, cell.NumberItems);
                        }
                    }
                }
            }
        }

        public void Initialize(InventoryGrass inventory, InventoryActiveGrass inventoryActive, GameObject window)
        {
            _inventoryGrass = inventory;
            _inventoryActiveGrass = inventoryActive;
            _window = window;
        }
    }
}