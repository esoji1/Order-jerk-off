using _Project.Improvements;
using _Project.Inventory.Items;
using _Project.ScriptableObjects.Configs;
using _Project.Weapon;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Inventory
{
    public class ChangeItem : MonoBehaviour
    {
        [SerializeField] private ItemData _data;
        [SerializeField] private Button _putOnButton;
        [SerializeField] private Button _takeOffButton;

        private Inventory _inventory;
        private InventoryActive _inventoryActive;
        private InventoryActivePotions _inventoryActivePotions;
        private UseWeapons.UseWeapons _useWeapons;

        private bool _isOpen;
        private bool _isWeaponEquipped;
        private Cell _clickedCellInventoryActive;
        private Cell _clickedCellInventory;
        private Cell _clickedCellInventoryActivePotion;

        public event Action<Enum> OnAddPotion;
        public event Action<Enum> OnSubstractPotion;

        private void Update()
        {
            if (_inventoryActive.IsOpen && _isOpen == false)
            {
                UpdateCurrentWeaponInHand();
                _isOpen = true;
            }
        }

        public void Initialize(UseWeapons.UseWeapons useWeapons, Inventory inventory, InventoryActive inventoryActive, InventoryActivePotions inventoryActivePotions)
        {
            _useWeapons = useWeapons;
            _inventory = inventory;
            _inventoryActive = inventoryActive;
            _inventoryActivePotions = inventoryActivePotions;

            _inventory.OnClickedItem += ClicedItemInventory;
            _inventoryActive.OnClickedItem += ClicedItemInventoryActive;
            _inventoryActivePotions.OnClickedItem += ClicedItemInventoryActivePotions;

            _putOnButton.onClick.AddListener(PutOn);
            _takeOffButton.onClick.AddListener(TakeOff);
        }

        private void ClicedItemInventoryActive(Cell cell) =>
            _clickedCellInventoryActive = cell;

        private void ClicedItemInventory(Cell cell) =>
            _clickedCellInventory = cell;

        private void ClicedItemInventoryActivePotions(Cell cell) =>
            _clickedCellInventoryActivePotion = cell;

        private void UpdateCurrentWeaponInHand()
        {
            WeaponTypes weaponTypes = _useWeapons.Weapon.Config.WeaponTypes;
            _isWeaponEquipped = true;
            if (weaponTypes == WeaponTypes.WoodenSwordPlayer)
            {
                _inventoryActive.AddItemInCell(_data.WoodenSwordItem);
                _useWeapons.SetWeapon(_data.WoodenSwordItem.GetItemType(), new ImprovementWeaponData());
            }
            else if (weaponTypes == WeaponTypes.WoodenAxePlayer)
            {
                _inventoryActive.AddItemInCell(_data.WoodenAxeItem);
                _useWeapons.SetWeapon(_data.WoodenAxeItem.GetItemType(), new ImprovementWeaponData());
            }
            else if (weaponTypes == WeaponTypes.WoodenOnionPlayer)
            {
                _inventoryActive.AddItemInCell(_data.WoodenOnionItem);
                _useWeapons.SetWeapon(_data.WoodenOnionItem.GetItemType(), new ImprovementWeaponData());
            }
        }

        private void PutOn()
        {
            if (_clickedCellInventory == null || _clickedCellInventory.Item == null)
                return;

            if (_clickedCellInventory.Item.Category == ItemCategory.Weapon)
            {
                if (_isWeaponEquipped)
                    return;

                WeaponItem weaponItem = _clickedCellInventory.Item as WeaponItem;

                _useWeapons.SetWeapon(_clickedCellInventory.Item.GetItemType(), weaponItem.ImprovementWeaponData);
                _inventoryActive.MoveItemToCell(_clickedCellInventory.Item, _clickedCellInventory);
                _isWeaponEquipped = true;
                return;
            }
            else if (_clickedCellInventory.Item.Category == ItemCategory.Potions)
            {
                OnAddPotion?.Invoke(_clickedCellInventory.Item.GetItemType());
                _inventoryActivePotions.AddItemInCell(_clickedCellInventory.Item);
                _inventory.SubtractItems(_clickedCellInventory, 1);
                return;
            }

            _inventoryActive.AddItemInCell(_clickedCellInventory.Item);
            _inventory.SubtractItems(_clickedCellInventory, 1);
        }

        private void TakeOff()
        {
            if(_clickedCellInventoryActivePotion != null && _clickedCellInventoryActivePotion.Item != null)
            {
                OnSubstractPotion?.Invoke(_clickedCellInventoryActivePotion.Item.GetItemType());
                _inventory.AddItemInCell(_clickedCellInventoryActivePotion.Item);
                _inventoryActivePotions.SubtractItems(_clickedCellInventoryActivePotion, 1);
                _clickedCellInventoryActivePotion = null;
                return;
            }

            if (_clickedCellInventoryActive == null || _clickedCellInventoryActive.Item == null)
                return;
            
            if (_clickedCellInventoryActive.Item.Category == ItemCategory.Weapon)
            {
                _inventory.MoveItemToCell(_clickedCellInventoryActive.Item, _clickedCellInventoryActive);
                _useWeapons.SetWeapon(null, null);
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