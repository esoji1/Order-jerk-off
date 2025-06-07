using System;
using UnityEngine;
using _Project.Core.HealthSystem;
using _Project.SelectionGags;
using Assets._Project.Scripts.Enemy;

namespace _Project.Enemy
{
    public class EnemyFactory
    {
        private EnemyConfig _planetPredator, _slime, _distant, _heavy, _wizard;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _uiDynamic;
        private Experience _experiencePrefab;
        private Coin _coinPrefab;
        private Transform[] _points;
        private Player.Player _player;

        public EnemyFactory(EnemyConfig planet, EnemyConfig slime, EnemyConfig distant, EnemyConfig heavy, EnemyConfig wizard,
            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas uiDynamic, Experience experiencePrefab,
            Coin coinPrefab, Transform[] points, Player.Player player)
        {
            _planetPredator = planet;
            _slime = slime;
            _distant = distant;
            _heavy = heavy;
            _wizard = wizard;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _uiDynamic = uiDynamic;
            _experiencePrefab = experiencePrefab;
            _coinPrefab = coinPrefab;
            _points = points;
            _player = player;
        }

        public Enemy Get(Enum type, Vector3 position)
        {
            EnemyConfig config = GetConfigBy(type);
            Enemy instance = UnityEngine.Object.Instantiate(config.Prefab, position, Quaternion.identity, null);
            Enemy baseEnemy = InitializeObject(instance, config, type);
            return baseEnemy;
        }

        private EnemyConfig GetConfigBy(Enum type)
        {
            switch (type)
            {
                case EnemyType.PlantPredator:
                    return _planetPredator;

                case EnemyType.Slime:
                    return _slime;

                case EnemyType.Distant:
                    return _distant;

                case EnemyType.Heavy:
                    return _heavy;

                case BossEnemyType.Wizard:
                    return _wizard;

                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        private Enemy InitializeObject(Enemy instance, EnemyConfig config, Enum type)
        {
            InitializePathSearch(instance, config);
            InitializeFoundObjectsNeedsPlayer(instance);

            switch (type)
            {
                case EnemyType.PlantPredator:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, EnemyType.PlantPredator, _experiencePrefab, 
                        _coinPrefab, _player);
                    return instance;

                case EnemyType.Slime:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, EnemyType.Slime, _experiencePrefab, 
                        _coinPrefab, _player);
                    return instance;

                case EnemyType.Distant:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, EnemyType.Distant, _experiencePrefab, 
                        _coinPrefab, _player);
                    return instance;

                case EnemyType.Heavy:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, EnemyType.Heavy, _experiencePrefab, 
                        _coinPrefab, _player);
                    return instance;

                case BossEnemyType.Wizard:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, BossEnemyType.Wizard, _experiencePrefab, 
                        _coinPrefab, _player);
                    return instance;

                default:
                    throw new ArgumentException(nameof(type));
            }
           
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
