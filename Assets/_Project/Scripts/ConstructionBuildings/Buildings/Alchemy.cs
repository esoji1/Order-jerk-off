using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public class Alchemy : BaseBuilding
    {
        private Sctipts.Inventory.Inventory _inventory;

        public void Initialize(GameObject window, Canvas staticCanvas, Player.Player player, Sctipts.Inventory.Inventory inventory)
        {
            base.Initialize(window, staticCanvas, player);

            _inventory = inventory;
        }
    }
}