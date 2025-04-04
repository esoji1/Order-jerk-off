using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class BuildingFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private House _housePrefab;
        [SerializeField] private Shop _shopPrefab;

        private BuildingFactory _buildingFactory;

        public BuildingFactory BuildingFactory => _buildingFactory;

        private void Awake()
        {
            _buildingFactory = new BuildingFactory(_housePrefab, _shopPrefab);
        }
    }
}
