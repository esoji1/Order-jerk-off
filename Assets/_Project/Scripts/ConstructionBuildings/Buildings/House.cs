using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Player.Pumping;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Inventory;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public class House : BaseBuilding
    {
        private CharacteristicsView _characteristicsView;
        private ChangeItem _changeItem;
        private SaleItem _saleItem;
        private UseWeapons.UseWeapons _useWeapons;
        private Sctipts.Inventory.Inventory _inventory;
        private InformationAboutItemView _informationAboutItemView;

        public void Initialize(GameObject playerHomeMenu, Canvas staticCanvas, Player.Player player, UseWeapons.UseWeapons useWeapons,
            Sctipts.Inventory.Inventory inventory)
        {
            base.Initialize(playerHomeMenu, staticCanvas, player);
            _inventory = inventory;
            _useWeapons = useWeapons;
            _characteristicsView = Window.GetComponentInChildren<CharacteristicsView>();
            _characteristicsView.Initialize(Player);
            _informationAboutItemView = Window.GetComponentInChildren<InformationAboutItemView>();
            _informationAboutItemView.Initialize(inventory);
            _inventory.Initialize(Window.GetComponentInChildren<PointContent>().GetComponent<RectTransform>());
            _changeItem = Window.GetComponentInChildren<ChangeItem>();
            _changeItem.Initialize(_useWeapons, inventory);
            _saleItem = Window.GetComponentInChildren<SaleItem>();
            _saleItem.Initialize(Player, inventory);
            Show();
        }
    }
}