using _Project.Craft;
using _Project.Inventory;
using _Project.Inventory.AlchemyInventory;
using _Project.ScriptableObjects.Configs;
using UnityEngine;

namespace _Project.ConstructionBuildings.Buildings
{
    public class Alchemy : BaseBuilding
    {
        private InventoryActiveGrass _inventoryActiveGrass;
        private InventoryGrass _inventoryGrass;
        private Inventory.Inventory _inventory;
        private Crafts _crafts;
        private ChangeItemGrass _changeItemGrass;
        private ControllInventoryGrass _controllInventoryGrass;

        public void Initialize(BuildsConfig config, Canvas staticCanvas, Player.Player player, Inventory.Inventory inventory, ControllInventoryGrass controllInventoryGrass)
        {
            base.Initialize(config, staticCanvas, player);

            _inventory = inventory;
            _controllInventoryGrass = controllInventoryGrass;

            _inventoryGrass = Window.GetComponentInChildren<InventoryGrass>();
            _inventoryGrass.Initialize(Window.GetComponentInChildren<InventoryGrass>().GetComponentsInChildren<Cell>(), _inventory);
            
            _inventoryActiveGrass = Window.GetComponentInChildren<InventoryActiveGrass>();
            _inventoryActiveGrass.Initialize(Window.GetComponentInChildren<InventoryActiveGrass>().GetComponentsInChildren<Cell>());

            _changeItemGrass = Window.GetComponentInChildren<ChangeItemGrass>();
            _changeItemGrass.Initialize(_inventoryGrass, _inventoryActiveGrass);

            _crafts = Window.GetComponentInChildren<Crafts>();
            _crafts.Initialize(_inventoryActiveGrass, _inventory);

            _controllInventoryGrass.Initialize(_inventoryGrass, _inventoryActiveGrass, Window);
        }
    }
}