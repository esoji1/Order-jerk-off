using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using Assets._Project.Scripts.Inventory;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class BuildingFactory
    {
        private House _housePrefab;
        private Shop _shopPrefab;
        private Alchemy _alchemyPrefab;
        private GameObject _playerHomeMenuPrefab;
        private GameObject _playerShopMenuPrefab;
        private GameObject _playerAlchemyMenuPrefab;
        private Canvas _staticCanvas;
        private Player.Player _player; 
        private UseWeapons.UseWeapons _useWeapons;
        private Sctipts.Inventory.Inventory _inventory;
        private InventoryActive _inventoryActive;

        public BuildingFactory(House housePrefab, Shop shopPrefab, Alchemy alchemyPrefab, GameObject playerHomeMenuPrefab, GameObject playerShopMenuPrefab, GameObject playerAlchemyMenuPrefab,
            Canvas staticCanvas, Player.Player player, UseWeapons.UseWeapons useWeapons, Sctipts.Inventory.Inventory inventory, InventoryActive inventoryActive)
        {
            _housePrefab = housePrefab;
            _shopPrefab = shopPrefab;
            _alchemyPrefab = alchemyPrefab;
            _playerHomeMenuPrefab = playerHomeMenuPrefab;
            _playerShopMenuPrefab = playerShopMenuPrefab;
            _playerAlchemyMenuPrefab = playerAlchemyMenuPrefab;
            _staticCanvas = staticCanvas;
            _player = player;
            _useWeapons = useWeapons;
            _inventory = inventory;
            _inventoryActive = inventoryActive;
        }

        public BaseBuilding Get(TypesBuildings typesBuildings, Vector3 position)
        {
            BaseBuilding building = GetSpawn(typesBuildings);
            BaseBuilding instance = UnityEngine.Object.Instantiate(building, position, Quaternion.identity, null);
            BaseBuilding baseBuilding = InitializeObject(instance);
            return baseBuilding;
        }

        private BaseBuilding GetSpawn(TypesBuildings typesBuildings)
        {
            switch (typesBuildings)
            {
                case TypesBuildings.House:
                    return _housePrefab;

                case TypesBuildings.Shop:
                    return _shopPrefab;

                case TypesBuildings.Alchemy:
                    return _alchemyPrefab;

                default:
                    throw new ArgumentException($"not {typesBuildings}");
            }
        }

        private BaseBuilding InitializeObject(BaseBuilding instance)
        {
            if (instance is House house)
            {
                house.Initialize(_playerHomeMenuPrefab, _staticCanvas, _player, _useWeapons, _inventory, _inventoryActive);
                return house;
            }
            else if(instance is Shop shop)
            {
                shop.Initialize(_playerShopMenuPrefab, _staticCanvas, _player, _inventory);
                return shop;
            }
            else if (instance is Alchemy alchemy)
            {
                alchemy.Initialize(_playerAlchemyMenuPrefab, _staticCanvas, _player, _inventory);
                return alchemy;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}