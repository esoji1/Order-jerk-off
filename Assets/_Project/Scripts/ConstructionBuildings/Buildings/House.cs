using _Project.Core;
using _Project.Core.Points;
using _Project.Inventory;
using _Project.Player.Pumping;
using _Project.ScriptableObjects.Configs;
using System.Linq;
using UnityEngine;

namespace _Project.ConstructionBuildings.Buildings
{
    public class House : BaseBuilding
    {
        private UseWeapons.UseWeapons _useWeapons;
        private Inventory.Inventory _inventory;
        private InventoryActive _inventoryActive;

        private CharacteristicsView _characteristicsView;
        private ChangeItem _changeItem;
        private SaleItem _saleItem;
        private InformationAboutItemView _informationAboutItemView;

        public void Initialize(BuildsConfig config, Canvas staticCanvas, Player.Player player, UseWeapons.UseWeapons useWeapons,
            Inventory.Inventory inventory, InventoryActive inventoryActive)
        {
            base.Initialize(config, staticCanvas, player);

            _inventory = inventory;
            _inventoryActive = inventoryActive;
            _useWeapons = useWeapons;

            _characteristicsView = Window.GetComponentInChildren<CharacteristicsView>();
            _characteristicsView.Initialize(Player);

            _informationAboutItemView = Window.GetComponentInChildren<InformationAboutItemView>();
            _informationAboutItemView.Initialize(inventory, inventoryActive);

            _inventory.Initialize(Window.GetComponentInChildren<PointContent>().GetComponent<RectTransform>(), 
                Window.GetComponentInChildren<PointContent>().GetComponentsInChildren<Cell>().ToList());
            _inventoryActive.Initialize(Window.GetComponentInChildren<PointContentActive>().GetComponent<RectTransform>(), 
                Window.GetComponentInChildren<PointContentActive>().GetComponentsInChildren<Cell>().ToList());

            _changeItem = Window.GetComponentInChildren<ChangeItem>();
            _changeItem.Initialize(_useWeapons, inventory, inventoryActive);

            _saleItem = Window.GetComponentInChildren<SaleItem>();
            _saleItem.Initialize(Player, inventory);
        }
    }
}