using Assets._Project.Scripts.Enemy.Enemys;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core.HealthSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class EnemyFactory
    {
        private EnemyConfig _plantPredator, _slime;
        private SelectionGags.Experience _experience;
        private SelectionGags.Coin _coin;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _dynamic;
        private LayerMask _layer;

        public EnemyFactory(EnemyConfig plantPredator, EnemyConfig slime, SelectionGags.Experience experience, SelectionGags.Coin coin, 
            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas dynamic, LayerMask layer)
        {
            _plantPredator = plantPredator;
            _slime = slime;
            _experience = experience;
            _coin = coin;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _dynamic = dynamic;
            _layer = layer;
        }

        public Enemy Get(EnemyTypes enemyType, Vector3 position, List<Transform> points)
        {
            EnemyConfig config = GetConfigBy(enemyType);
            Enemy instance = UnityEngine.Object.Instantiate(config.Prefab, position, Quaternion.identity, null);
            Enemy baseEnemy = InitializeObject(instance, config, points);
            return baseEnemy;
        }

        private EnemyConfig GetConfigBy(EnemyTypes types)
        {
            switch (types)
            {
                case EnemyTypes.PlantPredator:
                    return _plantPredator;

                case EnemyTypes.Slime:
                    return _slime;

                default:
                    throw new ArgumentException(nameof(types));
            }
        }

        private Enemy InitializeObject(Enemy instance, EnemyConfig config, List<Transform> points)
        {
            if (instance is CommonEnemy || instance is HeavyCommonEnemy)
            {
                instance.Initialize(config, _experience, _coin, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer, points);

                return instance;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}
