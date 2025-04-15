using Assets._Project.Scripts.Core.Points;
using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Player.Pumping;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Inventory;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public class House : BaseBuilding
    {
        private UseWeapons.UseWeapons _useWeapons;
        private Sctipts.Inventory.Inventory _inventory;
        private InventoryActive _inventoryActive;

        private CharacteristicsView _characteristicsView;
        private ChangeItem _changeItem;
        private SaleItem _saleItem;
        private InformationAboutItemView _informationAboutItemView;

        public void Initialize(GameObject playerHomeMenu, Canvas staticCanvas, Player.Player player, UseWeapons.UseWeapons useWeapons,
            Sctipts.Inventory.Inventory inventory, InventoryActive inventoryActive)
        {
            base.Initialize(playerHomeMenu, staticCanvas, player);

            _inventory = inventory;
            _inventoryActive = inventoryActive;
            _useWeapons = useWeapons;

            _characteristicsView = Window.GetComponentInChildren<CharacteristicsView>();
            _characteristicsView.Initialize(Player);

            _informationAboutItemView = Window.GetComponentInChildren<InformationAboutItemView>();
            _informationAboutItemView.Initialize(inventory, inventoryActive);

            _inventory.Initialize(Window.GetComponentInChildren<PointContent>().GetComponent<RectTransform>());
            _inventoryActive.Initialize(Window.GetComponentInChildren<PointContentActive>().GetComponent<RectTransform>());

            _changeItem = Window.GetComponentInChildren<ChangeItem>();
            _changeItem.Initialize(_useWeapons, inventory, inventoryActive);

            _saleItem = Window.GetComponentInChildren<SaleItem>();
            _saleItem.Initialize(Player, inventory);

            Show();
        }
    }
}