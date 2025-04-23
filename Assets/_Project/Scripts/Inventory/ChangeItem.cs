using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.UseWeapons;
using Assets._Project.Scripts.Weapon;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class ChangeItem : MonoBehaviour
    {
        [SerializeField] private ItemData _data;
        [SerializeField] private Button _putOnButton;
        [SerializeField] private Button _takeOffButton;

        private Inventory _inventory;
        private InventoryActive _inventoryActive;
        private UseWeapons _useWeapons;

        private bool _isOpen;
        private bool _isWeaponEquipped;
        private Cell _clickedCellInventoryActive;
        private Cell _clickedCellInventory;


        private void Update()
        {
            if (_inventoryActive.IsOpen && _isOpen == false)
            {
                UpdateCurrentWeaponInHand();
                _isOpen = true;
            }
        }

        public void Initialize(UseWeapons useWeapons, Inventory inventory, InventoryActive inventoryActive)
        {
            _useWeapons = useWeapons;
            _inventory = inventory;
            _inventoryActive = inventoryActive;

            _inventory.OnClickedItem += ClicedItemInventory;
            _inventoryActive.OnClickedItem += ClicedItemInventoryActive;

            _putOnButton.onClick.AddListener(PutOn);
            _takeOffButton.onClick.AddListener(TakeOff);
        }

        private void ClicedItemInventoryActive(Cell cell) =>
            _clickedCellInventoryActive = cell;

        private void ClicedItemInventory(Cell cell) =>
            _clickedCellInventory = cell;

        private void UpdateCurrentWeaponInHand()
        {
            WeaponTypes weaponTypes = _useWeapons.Weapon.Config.WeaponTypes;
            _isWeaponEquipped = true;
            if (weaponTypes == WeaponTypes.WoodenSwordPlayer)
                _inventoryActive.AddItemInCell(_data.WoodenSwordItem);
            else if (weaponTypes == WeaponTypes.WoodenSwordPlayer)
                _inventoryActive.AddItemInCell(_data.WoodenAxeItem);
            else if (weaponTypes == WeaponTypes.WoodenOnionPlayer)
                _inventoryActive.AddItemInCell(_data.WoodenOnionItem);
        }

        private void PutOn()
        {
            if (_clickedCellInventory == null || _clickedCellInventory.Item == null)
                return;

            if (_clickedCellInventory.Item.Category == ItemCategory.Weapon)
            {
                if (_isWeaponEquipped)
                    return;

                _inventoryActive.AddItemInCell(_clickedCellInventory.Item);
                _useWeapons.SetWeapon(_clickedCellInventory.Item.GetItemType());
                _inventory.SubtractItems(_clickedCellInventory, 1);
                _isWeaponEquipped = true;
                return;
            }

            _inventoryActive.AddItemInCell(_clickedCellInventory.Item);
            _inventory.SubtractItems(_clickedCellInventory, 1);
        }

        private void TakeOff()
        {
            if (_clickedCellInventoryActive == null || _clickedCellInventoryActive.Item == null)
                return;

            if (_clickedCellInventoryActive.Item.Category == ItemCategory.Weapon)
            {
                _inventory.AddItemInCell(_clickedCellInventoryActive.Item);
                _inventoryActive.SubtractItems(_clickedCellInventoryActive, 1);
                _isWeaponEquipped = false;
                return;
            }

            _inventory.AddItemInCell(_clickedCellInventoryActive.Item);
            _inventoryActive.SubtractItems(_clickedCellInventoryActive, 1);
        }

        private void OnDestroy()
        {
            _inventory.OnClickedItem -= ClicedItemInventory;
            _inventoryActive.OnClickedItem -= ClicedItemInventoryActive;

            _putOnButton.onClick.RemoveListener(PutOn);
            _takeOffButton.onClick.RemoveListener(TakeOff);
        }
    }
}