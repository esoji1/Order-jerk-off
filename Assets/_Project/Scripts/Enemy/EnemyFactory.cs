using System;
using UnityEngine;
using _Project.Core.HealthSystem;
using _Project.SelectionGags;
using _Project.ScriptableObjects;
using _Project.Enemy.Types;
using _Project.Enemy.Behaviors;
using _Project.Core.Interface;

namespace _Project.Enemy
{
    public class EnemyFactory
    {
        private EnemyConfig _planetPredator, _slime, _distant, _heavy, _orc, _wizard, _summoner;
        private EnemyConfig _planetMoveToTarget, _slimeMoveToTarget, _distantMoveToTarget, _heavyMoveToTarget, _wizardMoveToTarget;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _uiDynamic;
        private Experience _experiencePrefab;
        private Coin _coinPrefab;
        private Player.Player _player;
        private GivesData _givesData;
        private Transform _targetForMoveToTarget;

        public EnemyFactory(EnemyConfig planet, EnemyConfig slime, EnemyConfig distant, EnemyConfig heavy, EnemyConfig wizard, EnemyConfig summoner,
            EnemyConfig orc, EnemyConfig planetMoveToTarget, EnemyConfig slimeMoveToTarget, EnemyConfig distantMoveToTarget,
            EnemyConfig heavyMoveToTarget, EnemyConfig wizardMoveToPoint, HealthInfo healthInfoPrefab, HealthView healthViewPrefab,
            Canvas uiDynamic, Experience experiencePrefab, Coin coinPrefab, Player.Player player, GivesData givesData,
            Transform targetForMoveToTarget)
        {
            _planetPredator = planet;
            _slime = slime;
            _distant = distant;
            _heavy = heavy;
            _wizard = wizard;
            _summoner = summoner;
            _orc = orc;
            _planetMoveToTarget = planetMoveToTarget;
            _slimeMoveToTarget = slimeMoveToTarget;
            _distantMoveToTarget = distantMoveToTarget;
            _heavyMoveToTarget = heavyMoveToTarget;
            _wizardMoveToTarget = wizardMoveToPoint;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _uiDynamic = uiDynamic;
            _experiencePrefab = experiencePrefab;
            _coinPrefab = coinPrefab;
            _player = player;
            _givesData = givesData;
            _targetForMoveToTarget = targetForMoveToTarget;
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

                case EnemyType.Orc:
                    return _orc;

                case EnemyType.WizardBoss:
                    return _wizard;

                case EnemyType.SummonerBoss:
                    return _summoner;

                case EnemyType.PlantPredatorMoveToTarget:
                    return _planetMoveToTarget;

                case EnemyType.SlimeMoveToTarget:
                    return _slimeMoveToTarget;

                case EnemyType.DistantMoveToTarget:
                    return _distantMoveToTarget;

                case EnemyType.HeavyMoveToTarget:
                    return _heavyMoveToTarget;

                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        private Behaviors.Enemy InitializeObject(Behaviors.Enemy instance, EnemyConfig config, EnemyType type, Transform[] points)
        {
            InitializePathSearch(instance, config, points);
            InitializeFoundObjectsNeedsPlayer(instance);
            InitializeFoundObjectsNeedsMoveToTarget(instance);

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

                case EnemyType.Orc:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab,
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.WizardBoss:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab,
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.SummonerBoss:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab,
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.PlantPredatorMoveToTarget:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab,
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.SlimeMoveToTarget:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab,
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.DistantMoveToTarget:
                    instance.Initialize(_healthInfoPrefab, _healthViewPrefab, _uiDynamic, config, _experiencePrefab,
                        _coinPrefab, _player, _givesData);
                    return instance;

                case EnemyType.HeavyMoveToTarget:
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

        private void InitializeFoundObjectsNeedsMoveToTarget(Behaviors.Enemy instance)
        {
            IInitializeTarget[] initializeTargets = instance.GetComponents<IInitializeTarget>();

            foreach (IInitializeTarget initializeTarget in initializeTargets)
            {
                initializeTarget.Initialize(_targetForMoveToTarget);
            }
        }
    }
}
