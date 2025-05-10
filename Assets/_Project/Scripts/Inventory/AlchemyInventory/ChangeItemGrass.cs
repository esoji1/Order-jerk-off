using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory.AlchemyInventory
{
    public class ChangeItemGrass : MonoBehaviour
    {
        [SerializeField] private Button _putOnButton;
        [SerializeField] private Button _takeOffButton;

        private InventoryGrass _inventoryGrass;
        private InventoryActiveGrass _inventoryActiveGrass;

        private Cell _clickedCellInventoryActiveGrass;
        private Cell _clickedCellInventoryGrass;

        public void Initialize(InventoryGrass inventory, InventoryActiveGrass inventoryActive)
        {
            _inventoryGrass = inventory;
            _inventoryActiveGrass = inventoryActive;

            _inventoryGrass.OnClickedItem += ClicedItemInventoryGrass;
            _inventoryActiveGrass.OnClickedItem += ClicedItemInventoryActiveGrass;

            _putOnButton.onClick.AddListener(PutOn);
            _takeOffButton.onClick.AddListener(TakeOff);
        }

        private void ClicedItemInventoryActiveGrass(Cell cell) =>
            _clickedCellInventoryActiveGrass = cell;

        private void ClicedItemInventoryGrass(Cell cell) =>
            _clickedCellInventoryGrass = cell;

        private void PutOn()
        {
            if (_clickedCellInventoryGrass == null || _clickedCellInventoryGrass.Item == null)
                return;

            if (_clickedCellInventoryGrass.NumberItems > 0)
            {
                _inventoryActiveGrass.AddItemInCell(_clickedCellInventoryGrass.Item);
                _inventoryGrass.SubtractItems(_clickedCellInventoryGrass, 1);
            }
            _inventoryGrass.SetChangeValue(false);
        }

        private void TakeOff()
        {
            if (_clickedCellInventoryActiveGrass == null || _clickedCellInventoryActiveGrass.Item == null)
                return;

            _inventoryGrass.AddItemInCell(_clickedCellInventoryActiveGrass.Item, 1);
            _inventoryActiveGrass.SubtractItems(_clickedCellInventoryActiveGrass, 1);

            foreach(Cell cell in _inventoryActiveGrass.Cell)
            {
                if(cell != null)
                {
                    _inventoryGrass.SetChangeValue(false);
                }
                else
                {
                    _inventoryGrass.SetChangeValue(true);
                }
            }

        }

        private void OnDestroy()
        {
            _inventoryGrass.OnClickedItem -= ClicedItemInventoryGrass;
            _inventoryActiveGrass.OnClickedItem -= ClicedItemInventoryActiveGrass;

            _putOnButton.onClick.RemoveListener(PutOn);
            _takeOffButton.onClick.RemoveListener(TakeOff);
        }
    }
}