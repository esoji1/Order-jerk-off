//using _Project.Core.HealthSystem;
//using _Project.Enemy.Enemys;
//using _Project.ScriptableObjects.Configs;
//using _Project.Weapon.Projectile;
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace _Project.Enemy
//{
//    public class EnemyFactory
//    {
//        private ScriptableObjects.Configs.EnemyConfig _plantPredator, _slime, _magician, _heavyBlowEnemy;
//        private SelectionGags.Experience _experience;
//        private SelectionGags.Coin _coin;
//        private HealthInfo _healthInfoPrefab;
//        private HealthView _healthViewPrefab;
//        private Canvas _dynamic;
//        private LayerMask _layer;
//        private Transform _mainBuildingPoint;
//        private ProjectileEnemy _projectile;
//        private Player.Player _player;
//        private GameObject _circlePrimitiveHeavyAttack;

//        public EnemyFactory(ScriptableObjects.Configs.EnemyConfig plantPredator, ScriptableObjects.Configs.EnemyConfig slime, 
//            ScriptableObjects.Configs.EnemyConfig magician, ScriptableObjects.Configs.EnemyConfig heavyBlowEnemy, SelectionGags.Experience experience, SelectionGags.Coin coin, 
//            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas dynamic, LayerMask layer, Transform mainBuildingPoint,
//            ProjectileEnemy projectile, Player.Player player, GameObject circlePrimitiveHeavyAttack)
//        {
//            _plantPredator = plantPredator;
//            _slime = slime;
//            _magician = magician;
//            _heavyBlowEnemy = heavyBlowEnemy;
//            _experience = experience;
//            _coin = coin;
//            _healthInfoPrefab = healthInfoPrefab;
//            _healthViewPrefab = healthViewPrefab;
//            _dynamic = dynamic;
//            _layer = layer;
//            _mainBuildingPoint = mainBuildingPoint;
//            _projectile = projectile;
//            _player = player;
//            _circlePrimitiveHeavyAttack = circlePrimitiveHeavyAttack;
//        }

//        public Enemys.Enemy Get(EnemyType enemyType, Vector3 position, List<Transform> points, bool _isMoveRandomPoints)
//        {
//            ScriptableObjects.Configs.EnemyConfig config = GetConfigBy(enemyType);
//            Enemys.Enemy instance = UnityEngine.Object.Instantiate(config.Prefab, position, Quaternion.identity, null);
//            Enemys.Enemy baseEnemy = InitializeObject(instance, config, points, _isMoveRandomPoints);
//            return baseEnemy;
//        }

//        private ScriptableObjects.Configs.EnemyConfig GetConfigBy(EnemyType types)
//        {
//            switch (types)
//            {
//                case EnemyType.PlantPredator:
//                    return _plantPredator;

//                case EnemyType.Slime:
//                    return _slime;

//                case EnemyType.Magician:
//                    return _magician;

//                case EnemyType.HeavyBlow:
//                    return _heavyBlowEnemy;

//                default:
//                    throw new ArgumentException(nameof(types));
//            }
//        }

//        private Enemys.Enemy InitializeObject(Enemys.Enemy instance, ScriptableObjects.Configs.EnemyConfig config, List<Transform> points, bool _isMoveRandomPoints)
//        {
//            if (instance is PlantPredatorEnemy plantPredatorEnemy)
//            {
//                plantPredatorEnemy.Initialize(config, _experience, _coin, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer, points,
//                    _isMoveRandomPoints, _mainBuildingPoint, EnemyType.PlantPredator, _player);

//                return plantPredatorEnemy;
//            }
//            else if(instance is SlimeEnemy slimeEnemy)
//            {
//                slimeEnemy.Initialize(config, _experience, _coin, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer, points,
//                    _isMoveRandomPoints, _mainBuildingPoint, EnemyType.Slime, _player);

//                return slimeEnemy;
//            }
//            else if (instance is MagicianEnemy magicianEnemy)
//            {
//                magicianEnemy.Initialize(config, _experience, _coin, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer, points,
//                    _isMoveRandomPoints, _mainBuildingPoint, EnemyType.Magician, _player, _projectile);

//                return magicianEnemy;
//            }
//            else if (instance is HeavyBlowEnemy heavyBlowEnemy)
//            {
//                heavyBlowEnemy.Initialize(config, _experience, _coin, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer, points,
//                    _isMoveRandomPoints, _mainBuildingPoint, EnemyType.HeavyBlow, _player, _circlePrimitiveHeavyAttack);

//                return heavyBlowEnemy;
//            }
//            else
//            {
//                throw new ArgumentException(nameof(instance));
//            }
//        }
//    }
//}
