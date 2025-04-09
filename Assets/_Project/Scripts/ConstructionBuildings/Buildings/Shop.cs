using Assets._Project.Scripts.Inventory;
using Assets._Project.Sctipts.Inventory;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public class Shop : BaseBuilding
    {
        private Sctipts.Inventory.Inventory _inventory;

        private SellItemsStore _sellItemsStore;
        private InformationAboutItemShopView _informationAboutItemView;

        public void Initialize(GameObject window, Canvas staticCanvas, Player.Player player, Sctipts.Inventory.Inventory inventory)
        {
            base.Initialize(window, staticCanvas, player);

            _inventory = inventory;

            _sellItemsStore = Window.GetComponentInChildren<SellItemsStore>();
            _sellItemsStore.Initialize(Player, _inventory);

            _informationAboutItemView = Window.GetComponentInChildren<InformationAboutItemShopView>();
            _informationAboutItemView.Initialize(_sellItemsStore);
        }
    }
}