using Assets._Project.Scripts.Craft;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public class Alchemy : BaseBuilding
    {
        private Sctipts.Inventory.Inventory _inventory;
        private InventoryCrafting _inventoryCrafting;
        private Crafts _crafts;
        private ItemTextView _itemTextView;

        public void Initialize(BuildsConfig config, Canvas staticCanvas, Player.Player player, Sctipts.Inventory.Inventory inventory)
        {
            base.Initialize(config, staticCanvas, player);

            _inventory = inventory;

            _inventoryCrafting = Window.GetComponentInChildren<InventoryCrafting>();
            _inventoryCrafting.Initialize(_inventory);

            _crafts = Window.GetComponentInChildren<Crafts>();
            _crafts.Initialize(_inventoryCrafting);

            _itemTextView = Window.GetComponentInChildren<ItemTextView>();
            _itemTextView.Initialize(_inventoryCrafting);
        }
    }
}