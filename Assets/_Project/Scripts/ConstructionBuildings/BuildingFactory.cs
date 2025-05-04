using _Project.ConstructionBuildings.Buildings;
using _Project.ConstructionBuildings.DefensiveBuildings;
using _Project.Inventory;
using _Project.ScriptableObjects.Configs;
using _Project.Weapon.Projectile;
using System;
using TMPro;
using UnityEngine;

namespace _Project.ConstructionBuildings
{
    public class BuildingFactory
    {
        private BuildsConfig _houseConfig;
        private BuildsConfig _shopConfig;
        private BuildsConfig _alchemyConfig;
        private BuildsConfig _archerTowerConfig;
        private Canvas _staticCanvas;
        private Canvas _dynamicCanvas;
        private Player.Player _player; 
        private UseWeapons.UseWeapons _useWeapons;
        private Inventory.Inventory _inventory;
        private InventoryActive _inventoryActive;
        private TextMeshProUGUI _textDamage;
        private LayerMask _layerMask;
        private Projectile _projectile;

        public BuildingFactory(BuildsConfig housConfig, BuildsConfig shopConfig, BuildsConfig alchemyConfig, BuildsConfig archerTowerConfig, Canvas staticCanvas, 
            Player.Player player, UseWeapons.UseWeapons useWeapons,Inventory.Inventory inventory, InventoryActive inventoryActive, Canvas dynamicCanvas, TextMeshProUGUI textDamage,
            LayerMask layerMask, Projectile projectile)
        {
            _houseConfig = housConfig;
            _shopConfig = shopConfig;
            _alchemyConfig = alchemyConfig;
            _archerTowerConfig = archerTowerConfig;
            _staticCanvas = staticCanvas;
            _player = player;
            _useWeapons = useWeapons;
            _inventory = inventory;
            _inventoryActive = inventoryActive;
            _dynamicCanvas = dynamicCanvas;
            _textDamage = textDamage;
            _layerMask = layerMask;
            _projectile = projectile;
        }

        public BaseBuilding Get(TypesBuildings typesBuildings, Vector3 position)
        {
            BuildsConfig buildingConfig = GetSpawn(typesBuildings);
            BaseBuilding instance = UnityEngine.Object.Instantiate(buildingConfig.BaseBuildingPrefab, position, Quaternion.identity, null);
            BaseBuilding baseBuilding = InitializeObject(instance, buildingConfig);
            return baseBuilding;
        }

        private BuildsConfig GetSpawn(TypesBuildings typesBuildings)
        {
            switch (typesBuildings)
            {
                case TypesBuildings.House:
                    return _houseConfig;

                case TypesBuildings.Shop:
                    return _shopConfig;

                case TypesBuildings.Alchemy:
                    return _alchemyConfig;

               case TypesBuildings.ArcherTower:
                   return _archerTowerConfig;

                default:
                    throw new ArgumentException($"not {typesBuildings}");
            }
        }

        private BaseBuilding InitializeObject(BaseBuilding instance, BuildsConfig buildsConfig)
        {
            if (instance is House house)
            {
                house.Initialize(buildsConfig, _staticCanvas, _player, _useWeapons, _inventory, _inventoryActive);
                return house;
            }
            else if(instance is Shop shop)
            {
                shop.Initialize(buildsConfig, _staticCanvas, _player, _inventory);
                return shop;
            }
            else if (instance is Alchemy alchemy)
            {
                alchemy.Initialize(buildsConfig, _staticCanvas, _player, _inventory);
                return alchemy;
            }
            else if(instance is RangedAttackTower rangedAttackTower)
            {
                rangedAttackTower.Initialize(buildsConfig, _staticCanvas, _player, _inventory, _dynamicCanvas, _textDamage, _layerMask, _projectile);
                return rangedAttackTower;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}