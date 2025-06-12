using _Project.Artifacts;
using _Project.Core.Projectile;
using _Project.Inventory;
using _Project.Inventory.AlchemyInventory;
using _Project.Inventory.ForgeInventory;
using _Project.MapGeneration.Food;
using _Project.Potions;
using _Project.ScriptableObjects.Configs;
using TMPro;
using UnityEngine;

namespace _Project.ConstructionBuildings
{
    public class BuildingFactoryBootstrap : MonoBehaviour   
    {
        [SerializeField] private BuildsConfig _houseConfig, _shopConfig, _alchemyConfig, _archerTowerConfig, _forgeConfig;
        [SerializeField] private Canvas _staticCanvas;
        [SerializeField] private Player.Player _player;
        [SerializeField] private UseWeapons.UseWeapons _useWeapons;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private InventoryActive _inventoryActive;
        [SerializeField] private TextMeshProUGUI _textDamage;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Projectile _projectile; 
        [SerializeField] private Canvas _dynamicCanvas;
        [SerializeField] private InventoryActivePotions _inventoryActivePotions;
        [SerializeField] private ManagerPotion _managerPotion;
        [SerializeField] private ControllInventoryGrass _controllInventoryGrass;
        [SerializeField] private ControllInventoryForge _controllInventoryForge;
        [SerializeField] private Loss.Loss _loss;
        [SerializeField] private ManagerAtrefact _managerAtrefact;
        [SerializeField] private FoodView _foodView;

        private BuildingFactory _buildingFactory;

        public BuildingFactory BuildingFactory => _buildingFactory;

        private void Awake()
        {
            _buildingFactory = new BuildingFactory(_houseConfig, _shopConfig, _alchemyConfig, _archerTowerConfig, _forgeConfig, 
                _staticCanvas, _player, _useWeapons, _inventory, _inventoryActive, _dynamicCanvas, _textDamage, _layerMask, 
                _projectile, _inventoryActivePotions, _managerPotion, _controllInventoryGrass, _controllInventoryForge, _loss,
                _managerAtrefact, _foodView);
        }
    }
}
