using Assets._Project.Sctipts.Inventory;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public class Shop : BaseBuilding
    {
        private SellItemsStore _sellItemsStore;

        public void Initialize(GameObject window, Canvas staticCanvas, Player.Player player, BuildingFactoryBootstrap buildingFactoryBootstrap)
        {
            base.Initialize(window, staticCanvas, player);
            _sellItemsStore = Window.GetComponentInChildren<SellItemsStore>();
        }
    }
}