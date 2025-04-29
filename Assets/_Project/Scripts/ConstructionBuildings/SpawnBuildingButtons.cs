using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using Assets._Project.Scripts.ConstructionBuildings.DefensiveBuildings;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        [SerializeField] private Player.Player _player;

        [Header("Bottons")]
        [SerializeField] private Button _houseButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _alchemyButton;
        [SerializeField] private Button _archerTowerBotton;

        private BuildingArea _buildingArea;
        private List<BaseBuilding> _buildingList = new();

        private void Start()
        {
            _buildingArea = buildingAreaInHouse;
            Spawn(TypesBuildings.House, 0);
            _buildingArea = buildingAreaInShope;
            Spawn(TypesBuildings.Shop, 0);
        }

        private void OnEnable()
        {
            _actionButton.OnStandingInConstructionZone += AssignSpawnZone;

            _houseButton.onClick.AddListener(HouseButtonSpawn);
            _shopButton.onClick.AddListener(ShopButtonSpawn);
            _alchemyButton.onClick.AddListener(AlchemyButtonSpawn);
            _archerTowerBotton.onClick.AddListener(ArcherTowerButtonSpawn);

        }

        private void OnDisable()
        {
            _actionButton.OnStandingInConstructionZone -= AssignSpawnZone;

            _houseButton.onClick.RemoveListener(HouseButtonSpawn);
            _shopButton.onClick.RemoveListener(ShopButtonSpawn);
            _alchemyButton.onClick.RemoveListener(AlchemyButtonSpawn);
            _archerTowerBotton.onClick.RemoveListener(ArcherTowerButtonSpawn);
        }

        private void AssignSpawnZone(BuildingArea buildingArea)
        {
            _buildingArea = buildingArea;
        }

        private void HouseButtonSpawn()
        {
            if (IsTheZoneOccupied())
                return;

            Spawn(TypesBuildings.House, 0);
        }

        private void ShopButtonSpawn()
        {
            if (IsTheZoneOccupied())
                return;

            Spawn(TypesBuildings.Shop, 0);
        }

        private void AlchemyButtonSpawn()
        {
            if (IsTheZoneOccupied())
                return;

            Spawn(TypesBuildings.Alchemy, 20);
        }

        private void ArcherTowerButtonSpawn()
        {
            if (IsTheZoneOccupied())
                return;

            Spawn(TypesBuildings.ArcherTower, 0);
        }

        private bool IsTheZoneOccupied() => _buildingArea.IsZoneOccupied;

        private void Spawn(TypesBuildings typesBuildings, int value)
        {
            if (_player.Wallet.SubtractMoney(value) == false)
                return;

            BaseBuilding baseBuilding = _buildingFactoryBootstrap.BuildingFactory.Get(typesBuildings, _buildingArea.transform.position);

            foreach (BaseBuilding building in _buildingList)
            {
                if (building != null)
                {
                    if(baseBuilding.gameObject.TryGetComponent(out RangedAttackTower component))
                    {
                        continue;
                    }
                    else if (building.Type == baseBuilding.Type)
                    {
                        Destroy(baseBuilding.gameObject);
                        _buildingArea.SetZoneOccupeid(false);
                        _buildingArea.SetBaseBuilding(null);
                        _player.Wallet.AddMoney(value);
                        return;
                    }
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