using UnityEngine;

namespace _Project.Inventory.ForgeInventory
{
    public class ControllInventoryForge : MonoBehaviour
    {
        private Inventory _inventory;
        private InventoryForge _inventoryForge;
        private GameObject _window;

        private bool _isOpen;

        private void Update()
        {
            if (_window == null) 
                return;

            bool isWindowActive = _window.activeSelf;

            if (_isOpen != isWindowActive)
            {
                _isOpen = isWindowActive;

                if (_isOpen == false)
                    foreach (Cell cell in _inventoryForge.CellList)
                        _inventory.MoveItemToCell(cell.Item, cell);
                else
                    foreach(Cell cell in _inventory.CellList)
                        _inventoryForge.MoveItemToCell(cell.Item, cell);
            }
        }

        public void Initialize(Inventory inventory, InventoryForge inventoryForge, GameObject window)
        {
            _inventory = inventory;
            _inventoryForge = inventoryForge;
            _window = window;
        }
    }
}