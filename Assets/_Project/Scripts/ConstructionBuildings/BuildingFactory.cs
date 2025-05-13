using _Project.ConstructionBuildings.Buildings;
using _Project.ConstructionBuildings.DefensiveBuildings;
using _Project.Inventory;
using _Project.Inventory.AlchemyInventory;
using _Project.Potions;
using _Project.ScriptableObjects.Configs;
using _Project.Weapon.Projectile;
using System;
using TMPro;
using UnityEngine;

namespace _Project.ConstructionBuildings
{
    public class BuildingFactory
    {
        private BuildsConfig _houseConfig, _shopConfig, _alchemyConfig, _archerTowerConfig, _forgeConfig;
        private Canvas _staticCanvas;
        private Canvas _dynamicCanvas;
        private Player.Player _player; 
        private UseWeapons.UseWeapons _useWeapons;
        private Inventory.Inventory _inventory;
        private InventoryActivePotions _inventoryActivePotions;
        private InventoryActive _inventoryActive;
        private TextMeshProUGUI _textDamage;
        private LayerMask _layerMask;
        private Projectile _projectile;
        private ManagerPotion _managerPotion;
        private ControllInventoryGrass _controllInventoryGrass;

        public BuildingFactory(BuildsConfig housConfig, BuildsConfig shopConfig, BuildsConfig alchemyConfig, BuildsConfig archerTowerConfig, BuildsConfig forgeConfig,
            Canvas staticCanvas, Player.Player player, UseWeapons.UseWeapons useWeapons,Inventory.Inventory inventory,
            InventoryActive inventoryActive, Canvas dynamicCanvas, TextMeshProUGUI textDamage, LayerMask layerMask, Projectile projectile, 
            InventoryActivePotions inventoryActivePotions, ManagerPotion managerPotion, ControllInventoryGrass controllInventoryGrass)
        {
            _houseConfig = housConfig;
            _shopConfig = shopConfig;
            _alchemyConfig = alchemyConfig;
            _archerTowerConfig = archerTowerConfig;
            _forgeConfig = forgeConfig;
            _staticCanvas = staticCanvas;
            _player = player;
            _useWeapons = useWeapons;
            _inventory = inventory;
            _inventoryActive = inventoryActive;
            _dynamicCanvas = dynamicCanvas;
            _textDamage = textDamage;
            _layerMask = layerMask;
            _projectile = projectile;
            _inventoryActivePotions = inventoryActivePotions;
            _managerPotion = managerPotion;
            _controllInventoryGrass = controllInventoryGrass;
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

                case TypesBuildings.Forge:
                    return _forgeConfig;

                default:
                    throw new ArgumentException($"not {typesBuildings}");
            }
        }

        private BaseBuilding InitializeObject(BaseBuilding instance, BuildsConfig buildsConfig)
        {
            if (instance is House house)
            {
                house.Initialize(buildsConfig, _staticCanvas, _player, _useWeapons, _inventory, _inventoryActive, _inventoryActivePotions, _managerPotion);
                return house;
            }
            else if(instance is Shop shop)
            {
                shop.Initialize(buildsConfig, _staticCanvas, _player, _inventory);
                return shop;
            }
            else if (instance is Alchemy alchemy)
            {
                alchemy.Initialize(buildsConfig, _staticCanvas, _player, _inventory, _controllInventoryGrass);
                return alchemy;
            }
            else if(instance is RangedAttackTower rangedAttackTower)
            {
                rangedAttackTower.Initialize(buildsConfig, _staticCanvas, _player, _inventory, _dynamicCanvas, _textDamage, _layerMask, _projectile);
                return rangedAttackTower;
            }
            else if (instance is Forge forge)
            {
                forge.Initialize(buildsConfig, _staticCanvas, _player, _inventory);
                return forge;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}