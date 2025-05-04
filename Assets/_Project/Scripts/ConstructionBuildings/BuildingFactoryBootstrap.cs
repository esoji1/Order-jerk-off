using _Project.Inventory;
using _Project.ScriptableObjects.Configs;
using _Project.Weapon.Projectile;
using TMPro;
using UnityEngine;

namespace _Project.ConstructionBuildings
{
    public class BuildingFactoryBootstrap : MonoBehaviour   
    {
        [SerializeField] private BuildsConfig _houseConfig;
        [SerializeField] private BuildsConfig _shopConfig;
        [SerializeField] private BuildsConfig _alchemyConfig;
        [SerializeField] private BuildsConfig _archerTowerConfig;
        [SerializeField] private Canvas _staticCanvas;
        [SerializeField] private Player.Player _player;
        [SerializeField] private UseWeapons.UseWeapons _useWeapons;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private InventoryActive _inventoryActive;
        [SerializeField] private TextMeshProUGUI _textDamage;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Projectile _projectile; 
        [SerializeField] private Canvas _dynamicCanvas;

        private BuildingFactory _buildingFactory;

        public BuildingFactory BuildingFactory => _buildingFactory;

        private void Awake()
        {
            _buildingFactory = new BuildingFactory(_houseConfig, _shopConfig, _alchemyConfig, _archerTowerConfig, _staticCanvas, _player, _useWeapons, _inventory, _inventoryActive, 
                _dynamicCanvas, _textDamage, _layerMask, _projectile);
        }
    }
}
