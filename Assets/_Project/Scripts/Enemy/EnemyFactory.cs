using System;
using UnityEngine;
using _Project.Core.HealthSystem;
using _Project.SelectionGags;
using _Project.ScriptableObjects;
using _Project.Enemy.Types;
using _Project.Enemy.Behaviors;
using _Project.Core.Interface;
using _Project.ScriptableObjects.Configs;

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
        private Player.Player _player;
        private GivesData _givesData;

        public EnemyFactory(EnemyConfig planet, EnemyConfig slime, EnemyConfig distant, EnemyConfig heavy, EnemyConfig wizard,
            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas uiDynamic, Experience experiencePrefab,
            Coin coinPrefab, Player.Player player, GivesData givesData)
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
            _player = player;
            _givesData = givesData;
        }

        public Behaviors.Enemy Get(EnemyType type, Vector3 position, Transform[] points)
        {
            EnemyConfig config = GetConfigBy(type);
            Behaviors.Enemy instance = UnityEngine.Object.Instantiate(config.Prefab, position, Quaternion.identity, null);
            Behaviors.Enemy baseEnemy = InitializeObject(instance, config, type, points);
            return baseEnemy;
        }

        private EnemyConfig GetConfigBy(EnemyType type)
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

                case EnemyType.Wizard:
                    return _wizard;

                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        private Behaviors.Enemy InitializeObject(Behaviors.Enemy instance, EnemyConfig config, EnemyType type, Transform[] points)
        {
            InitializePathSearch(instance, config, points);
            InitializeFoundObjectsNeedsPlayer(instance);

            switch (type)
            {
                case EnemyType.PlantPredator:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab, 
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.Slime:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab, 
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.Distant:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab, 
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.Heavy:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab, 
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.Wizard:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab, 
                        _coinPrefab, _player, _givesData);
                    return instance;

                default:
                    throw new ArgumentException(nameof(type));
            }
           
        }

        private void InitializePathSearch(Behaviors.Enemy instance, EnemyConfig config, Transform[] points)
        {
            foreach (MovementBreakReasonType type in config.MovementBreakReason)
            {
                if (type.Equals(MovementBreakReasonType.Patrol))
                {
                    instance.GetComponent<Patrol>().Initialize(points);
                }
            }
        }

        private void InitializeFoundObjectsNeedsPlayer(Behaviors.Enemy instance)
        {
            IInitializePlayer[] initializePlayers = instance.GetComponents<IInitializePlayer>();

            foreach (IInitializePlayer initializePlayer in initializePlayers)
            {
                initializePlayer.Initialize(_player);
            }
        }
    }
}
