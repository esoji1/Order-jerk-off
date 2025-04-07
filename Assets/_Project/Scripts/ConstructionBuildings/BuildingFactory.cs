using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class BuildingFactory
    {
        private House _housePrefab;
        private Shop _shopPrefab;
        private GameObject _playerHomeMenuPrefab;
        private GameObject _playerShopMenuPrefab;
        private Canvas _staticCanvas;
        private Player.Player _player; 
        private UseWeapons.UseWeapons _useWeapons;
        private Sctipts.Inventory.Inventory _inventory;

        public BuildingFactory(House housePrefab, Shop shopPrefab, GameObject playerHomeMenuPrefab, GameObject playerShopMenuPrefab, 
            Canvas staticCanvas, Player.Player player, UseWeapons.UseWeapons useWeapons, Sctipts.Inventory.Inventory inventory)
        {
            _housePrefab = housePrefab;
            _shopPrefab = shopPrefab;
            _playerHomeMenuPrefab = playerHomeMenuPrefab;
            _playerShopMenuPrefab = playerShopMenuPrefab;
            _staticCanvas = staticCanvas;
            _player = player;
            _useWeapons = useWeapons;
            _inventory = inventory;
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

                default:
                    throw new ArgumentException($"not {typesBuildings}");
            }
        }

        private BaseBuilding InitializeObject(BaseBuilding instance)
        {
            if (instance is House house)
            {
                house.Initialize(_playerHomeMenuPrefab, _staticCanvas, _player, _useWeapons, _inventory);
                return house;
            }
            else if(instance is Shop shop)
            {
                shop.Initialize(_playerShopMenuPrefab, _staticCanvas, _player, _inventory);
                return shop;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}