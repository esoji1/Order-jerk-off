using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class BuildingFactory
    {
        private House _housePrefab;
        private Shop _shopPrefab;

        public BuildingFactory(House housePrefab, Shop shopPrefab)
        {
            _housePrefab = housePrefab;
            _shopPrefab = shopPrefab;
        }

        public BaseBuilding Get(TypesBuildings typesBuildings, Vector3 position)
        {
            BaseBuilding building = GetSpawn(typesBuildings);
            BaseBuilding instance = UnityEngine.Object.Instantiate(building, position, Quaternion.identity, null);
            return instance;
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
    }
}