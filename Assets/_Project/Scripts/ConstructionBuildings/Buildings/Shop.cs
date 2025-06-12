using _Project.Inventory;
using _Project.MapGeneration.Food;
using _Project.ScriptableObjects.Configs;
using UnityEngine;

namespace _Project.ConstructionBuildings.Buildings
{
    public class Shop : BaseBuilding
    {
        private Inventory.Inventory _inventory;

        private SellItemsStore _sellItemsStore;
        private InformationAboutItemShopView _informationAboutItemView;
        private FoodView _foodView;

        public void Initialize(BuildsConfig config, Canvas staticCanvas, Player.Player player, Inventory.Inventory inventory, 
            FoodView foodView)
        {
            base.Initialize(config, staticCanvas, player);

            _inventory = inventory;
            _foodView = foodView;

            _sellItemsStore = Window.GetComponentInChildren<SellItemsStore>();
            _sellItemsStore.Initialize(Player, _inventory, _foodView);

            _informationAboutItemView = Window.GetComponentInChildren<InformationAboutItemShopView>();
            _informationAboutItemView.Initialize(_sellItemsStore);
        }
    }
}