using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class BuildingFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private House _housePrefab;
        [SerializeField] private Shop _shopPrefab;
        [SerializeField] private GameObject _playerHomeMenuPrefab;
        [SerializeField] private GameObject _playerShopMenuPrefab;
        [SerializeField] private Canvas _staticCanvas;
        [SerializeField] private Player.Player _player;
        [SerializeField] private UseWeapons.UseWeapons _useWeapons;
        [SerializeField] private Sctipts.Inventory.Inventory _inventory;

        private BuildingFactory _buildingFactory;

        public BuildingFactory BuildingFactory => _buildingFactory;

        private void Awake()
        {
            _buildingFactory = new BuildingFactory(_housePrefab, _shopPrefab, _playerHomeMenuPrefab, _playerShopMenuPrefab, 
                _staticCanvas, _player, _useWeapons, _inventory);
        }
    }
}
