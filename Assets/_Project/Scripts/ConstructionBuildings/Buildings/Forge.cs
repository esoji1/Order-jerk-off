using _Project.Core;
using _Project.Inventory;
using _Project.Inventory.ForgeInventory;
using _Project.ScriptableObjects.Configs;
using UnityEngine;
using System.Linq;

namespace _Project.ConstructionBuildings.Buildings
{
    public class Forge : BaseBuilding
    {
        private Inventory.Inventory _inventory;
        private InventoryForge _inventoryForge;
        private InventoryOre _inventoryOre;
        private Improvements.Improvements _improvements;

        public void Initialize(BuildsConfig config, Canvas staticCanvas, Player.Player player, Inventory.Inventory inventory)
        {
            base.Initialize(config, staticCanvas, player);

            _inventory = inventory;

            _inventoryForge = Window.GetComponentInChildren<InventoryForge>();
            _inventoryForge.Initialize(Window.GetComponentInChildren<PointContent>().GetComponent<RectTransform>(), 
                Window.GetComponentInChildren<InventoryForge>().GetComponentsInChildren<Cell>().ToList(), _inventory);

            _inventoryOre = Window.GetComponentInChildren<InventoryOre>();
            _inventoryOre.Initialize(Window.GetComponentInChildren<InventoryOre>().GetComponentsInChildren<Cell>(), _inventory);

            _improvements = Window.GetComponentInChildren<Improvements.Improvements>();
            _improvements.Initialize(_inventory, _inventoryForge);
        }
    }
}