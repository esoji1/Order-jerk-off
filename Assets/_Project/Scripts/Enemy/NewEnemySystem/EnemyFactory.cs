using System;
using UnityEngine;
using _Project.Core.HealthSystem;
using _Project.SelectionGags;
using Assets._Project.Scripts.Enemy;

namespace _Project.Enemy
{
    public class EnemyFactory
    {
        private EnemyConfig _planetPredator, _slime, _distant;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _uiDynamic;
        private Experience _experiencePrefab;
        private Coin _coinPrefab;
        private Transform[] _points;
        private Player.Player _player;

        public EnemyFactory(EnemyConfig planet, EnemyConfig slime, EnemyConfig distant, HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas uiDynamic, Experience experiencePrefab,
            Coin coinPrefab, Transform[] points, Player.Player player)
        {
            _planetPredator = planet;
            _slime = slime;
            _distant = distant;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _uiDynamic = uiDynamic;
            _experiencePrefab = experiencePrefab;
            _coinPrefab = coinPrefab;
            _points = points;
            _player = player;
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

                case EnemyTypes.Distant:
                    return _distant;

                default:
                    throw new ArgumentException(nameof(types));
            }
        }

        private Enemy InitializeObject(Enemy instance, EnemyConfig config)
        {
            InitializePathSearch(instance, config);
            InitializeFoundObjectsNeedsPlayer(instance);

            instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, EnemyTypes.PlantPredator, _experiencePrefab, _coinPrefab);
            return instance;
        }

        private void InitializePathSearch(Enemy instance, EnemyConfig config)
        {
            foreach (MovementBreakReasonType type in config.MovementBreakReason)
            {
                if (type.Equals(MovementBreakReasonType.Patrol))
                {
                    instance.GetComponent<Patrol>().Initialize(_points);
                }
            }
        }

        private void InitializeFoundObjectsNeedsPlayer(Enemy instance)
        {
            IInitializePlayer[] initializePlayers = instance.GetComponents<IInitializePlayer>();

            foreach (IInitializePlayer initializePlayer in initializePlayers)
            {
                initializePlayer.Initialize(_player);
            }
        }
    }
}
