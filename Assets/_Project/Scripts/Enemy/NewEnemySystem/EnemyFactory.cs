using System;
using UnityEngine;
using _Project.Core.HealthSystem;
using _Project.SelectionGags;

namespace _Project.Enemy
{
    public class EnemyFactory
    {
        private EnemyConfig _planetPredator, _slime;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _uiDynamic;
        private Experience _experiencePrefab;
        private Coin _coinPrefab;
        private Transform[] _points;

        public EnemyFactory(EnemyConfig planet, EnemyConfig slime, HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas uiDynamic, Experience experiencePrefab,
            Coin coinPrefab, Transform[] points)
        {
            _planetPredator = planet;
            _slime = slime;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _uiDynamic = uiDynamic;
            _experiencePrefab = experiencePrefab;
            _coinPrefab = coinPrefab;
            _points = points;
        }

        public Enemy Get(EnemyTypes enemyType, Vector3 position)
        {
            EnemyConfig config = GetConfigBy(enemyType);
            Enemy instance = UnityEngine.Object.Instantiate(config.Prefab, position, Quaternion.identity, null);
            Enemy baseEnemy = InitializeObject(instance, config);
            return baseEnemy;
        }

        private EnemyConfig GetConfigBy(EnemyTypes types)
        {
            switch (types)
            {
                case EnemyTypes.PlantPredator:
                    return _planetPredator;

                case EnemyTypes.Slime:
                    return _slime;

                default:
                    throw new ArgumentException(nameof(types));
            }
        }

        private Enemy InitializeObject(Enemy instance, EnemyConfig config)
        {
            foreach (MovementBreakReasonType type in config.MovementBreakReason)
            {
                if (type.Equals(MovementBreakReasonType.Patrol))
                {
                    instance.GetComponent<Patrol>().Initialize(_points);
                }
            }

            instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, EnemyTypes.PlantPredator, _experiencePrefab, _coinPrefab);
            return instance;
        }
    }
}
