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
        private Canvas _staticCanvas;
        private Player.Player _player; 
        private UseWeapons.UseWeapons _useWeapons;

        public BuildingFactory(House housePrefab, Shop shopPrefab, GameObject playerHomeMenuPrefab, Canvas staticCanvas, Player.Player player,
            UseWeapons.UseWeapons useWeapons)
        {
            _housePrefab = housePrefab;
            _shopPrefab = shopPrefab;
            _playerHomeMenuPrefab = playerHomeMenuPrefab;
            _staticCanvas = staticCanvas;
            _player = player;
            _useWeapons = useWeapons;
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
                house.Initialize(_playerHomeMenuPrefab, _staticCanvas, _player, _useWeapons);
                return house;
            }
            else if(instance is Shop shop)
            {
                shop.Initialize(null, _staticCanvas);
                return shop;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}