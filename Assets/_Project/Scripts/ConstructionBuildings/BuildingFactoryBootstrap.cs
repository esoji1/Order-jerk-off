using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using Assets._Project.Scripts.Inventory;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class BuildingFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private House _housePrefab;
        [SerializeField] private Shop _shopPrefab;
        [SerializeField] private Alchemy _alchemyPrefab;
        [SerializeField] private GameObject _playerHomeMenuPrefab;
        [SerializeField] private GameObject _playerShopMenuPrefab;
        [SerializeField] private GameObject _playerAlchemyMenuPrefab;
        [SerializeField] private Canvas _staticCanvas;
        [SerializeField] private Player.Player _player;
        [SerializeField] private UseWeapons.UseWeapons _useWeapons;
        [SerializeField] private Sctipts.Inventory.Inventory _inventory;
        [SerializeField] private InventoryActive _inventoryActive;

        private BuildingFactory _buildingFactory;

        public BuildingFactory BuildingFactory => _buildingFactory;

        private void Awake()
        {
            _buildingFactory = new BuildingFactory(_housePrefab, _shopPrefab, _alchemyPrefab, _playerHomeMenuPrefab, _playerShopMenuPrefab, _playerAlchemyMenuPrefab, 
                _staticCanvas, _player, _useWeapons, _inventory, _inventoryActive);
        }
    }
}
