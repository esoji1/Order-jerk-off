using _Project.ConstructionBuildings.Buildings;
using _Project.ConstructionBuildings.DefensiveBuildings;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.ConstructionBuildings
{
    public class SpawnBuildingButtons : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ActionButton.ActionButton _actionButton;
        [SerializeField] private BuildingFactoryBootstrap _buildingFactoryBootstrap;
        [SerializeField] private BuildingArea buildingAreaInHouse;
        [SerializeField] private BuildingArea buildingAreaInShope;
        [SerializeField] private Player.Player _player;

        [Header("Buttons for purchasing buildings")]
        [SerializeField] private Button _houseButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _alchemyButton;
        [SerializeField] private Button _archerTowerBotton;
        [SerializeField] private Button _forgeBotton;

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
            _forgeBotton.onClick.AddListener(ForgeButtonSpawn);
        }

        private void OnDisable()
        {
            _actionButton.OnStandingInConstructionZone -= AssignSpawnZone;

            _houseButton.onClick.RemoveListener(HouseButtonSpawn);
            _shopButton.onClick.RemoveListener(ShopButtonSpawn);
            _alchemyButton.onClick.RemoveListener(AlchemyButtonSpawn);
            _archerTowerBotton.onClick.RemoveListener(ArcherTowerButtonSpawn);
            _forgeBotton.onClick.RemoveListener(ForgeButtonSpawn);
        }

        private void AssignSpawnZone(BuildingArea buildingArea) => _buildingArea = buildingArea;

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

            Spawn(TypesBuildings.ArcherTower, 15);
        }

        private void ForgeButtonSpawn()
        {
            if (IsTheZoneOccupied())
                return;

            Spawn(TypesBuildings.Forge, 40);
        }

        private bool IsTheZoneOccupied() => _buildingArea.IsZoneOccupied;

        private void Spawn(TypesBuildings typesBuildings, int value)
        {
            if (IsBuildAttackTowerField(typesBuildings))
                return;
            else if (IsBuildDevelopingTowerField(typesBuildings))
                return;
            if (_player.Wallet.SubtractMoney(value) == false)
                return;

            BaseBuilding baseBuilding = _buildingFactoryBootstrap.BuildingFactory.Get(typesBuildings, _buildingArea.transform.position);

            foreach (BaseBuilding building in _buildingList)
            {
                if (building != null)
                {
                    if (baseBuilding.gameObject.TryGetComponent(out RangedAttackTower component))
                        continue;

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
                baseBuilding.SpriteRenderer.flipX = false;
            else if (_buildingArea.LeftOrRightSide == -1)
                baseBuilding.SpriteRenderer.flipX = true;
        }

        private bool IsBuildAttackTowerField(TypesBuildings typesBuildings)
        {
            if (_buildingArea.TryGetComponent(out PointTower _) && typesBuildings != TypesBuildings.ArcherTower)
                return true;

            return false;
        }

        private bool IsBuildDevelopingTowerField(TypesBuildings typesBuildings)
        {
            if (_buildingArea.TryGetComponent(out PointTower _) == false && typesBuildings == TypesBuildings.ArcherTower)
                return true;

            return false;
        }
    }
}