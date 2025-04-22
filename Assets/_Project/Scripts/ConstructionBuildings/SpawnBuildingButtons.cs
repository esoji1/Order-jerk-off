using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class SpawnBuildingButtons : MonoBehaviour
    {
        [SerializeField] private ActionButton.ActionButton _actionButton;
        [SerializeField] private BuildingFactoryBootstrap _buildingFactoryBootstrap;
        [SerializeField] private BuildingArea buildingAreaInHouse;
        [SerializeField] private BuildingArea buildingAreaInShope;

        [Header("Bottons")]
        [SerializeField] private Button _houseButton;
        [SerializeField] private Button _shopButton;

        private BuildingArea _buildingArea;
        private List<BaseBuilding> _buildingList = new();

        private void Start()
        {
            _buildingArea = buildingAreaInHouse;
            Spawn(TypesBuildings.House);
            _buildingArea = buildingAreaInShope;
            Spawn(TypesBuildings.Shop);
        }

        private void OnEnable()
        {
            _actionButton.OnStandingInConstructionZone += AssignSpawnZone;

            _houseButton.onClick.AddListener(HouseButtonSpawn);
            _shopButton.onClick.AddListener(ShopButtonSpawn);
        }

        private void OnDisable()
        {
            _actionButton.OnStandingInConstructionZone -= AssignSpawnZone;

            _houseButton.onClick.RemoveListener(HouseButtonSpawn);
            _shopButton.onClick.RemoveListener(ShopButtonSpawn);
        }

        private void AssignSpawnZone(BuildingArea buildingArea)
        {
            _buildingArea = buildingArea;
        }

        private void HouseButtonSpawn()
        {
            if (IsTheZoneOccupied())
                return;

            Spawn(TypesBuildings.House);
        }

        private void ShopButtonSpawn()
        {
            if (IsTheZoneOccupied())
                return;

            Spawn(TypesBuildings.Shop);
        }

        private bool IsTheZoneOccupied() => _buildingArea.IsZoneOccupied;

        private void Spawn(TypesBuildings typesBuildings)
        {
            BaseBuilding baseBuilding = _buildingFactoryBootstrap.BuildingFactory.Get(typesBuildings, _buildingArea.transform.position);

            foreach (BaseBuilding building in _buildingList)
            {
                if (building.Type == baseBuilding.Type)
                {
                    Destroy(baseBuilding.gameObject);
                    _buildingArea.SetZoneOccupeid(false);
                    _buildingArea.SetBaseBuilding(null);
                    return;
                }
            }

            _buildingList.Add(baseBuilding);
            _buildingArea.SetZoneOccupeid(true);
            _buildingArea.SetBaseBuilding(baseBuilding);
            Flip(baseBuilding);
        }

        private void Flip(BaseBuilding baseBuilding)
        {
            if (_buildingArea.LeftOrRightSide == 1)
            {
                baseBuilding.SpriteRenderer.flipX = false;
            }
            else if (_buildingArea.LeftOrRightSide == -1)
            {
                baseBuilding.SpriteRenderer.flipX = true;
            }
        }
    }
}