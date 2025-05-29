using _Project.Core.HealthSystem;
using _Project.Enemy.Enemys;
using _Project.ScriptableObjects.Configs;
using _Project.Weapon.Projectile;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Enemy
{
    public class EnemyFactory
    {
        private EnemyConfig _plantPredator, _slime, _magician;
        private SelectionGags.Experience _experience;
        private SelectionGags.Coin _coin;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _dynamic;
        private LayerMask _layer;
        private Transform _mainBuildingPoint;
        private ProjectileEnemy _projectile;
        private Player.Player _player;

        public EnemyFactory(EnemyConfig plantPredator, EnemyConfig slime,EnemyConfig magician, SelectionGags.Experience experience, SelectionGags.Coin coin, 
            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas dynamic, LayerMask layer, Transform mainBuildingPoint,
            ProjectileEnemy projectile, Player.Player player)
        {
            _plantPredator = plantPredator;
            _slime = slime;
            _magician = magician;
            _experience = experience;
            _coin = coin;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _dynamic = dynamic;
            _layer = layer;
            _mainBuildingPoint = mainBuildingPoint;
            _projectile = projectile;
            _player = player;
        }

        public Enemys.Enemy Get(EnemyTypes enemyType, Vector3 position, List<Transform> points, bool _isMoveRandomPoints)
        {
            EnemyConfig config = GetConfigBy(enemyType);
            Enemys.Enemy instance = UnityEngine.Object.Instantiate(config.Prefab, position, Quaternion.identity, null);
            Enemys.Enemy baseEnemy = InitializeObject(instance, config, points, _isMoveRandomPoints);
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

                case EnemyTypes.Magician:
                    return _magician;

                default:
                    throw new ArgumentException(nameof(types));
            }
        }

        private Enemys.Enemy InitializeObject(Enemys.Enemy instance, EnemyConfig config, List<Transform> points, bool _isMoveRandomPoints)
        {
            if (instance is PlantPredatorEnemy plantPredatorEnemy)
            {
                plantPredatorEnemy.Initialize(config, _experience, _coin, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer, points,
                    _isMoveRandomPoints, _mainBuildingPoint, EnemyTypes.PlantPredator, _player);

                return plantPredatorEnemy;
            }
            else if(instance is SlimeEnemy slimeEnemy)
            {
                slimeEnemy.Initialize(config, _experience, _coin, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer, points,
                    _isMoveRandomPoints, _mainBuildingPoint, EnemyTypes.Slime, _player);

                return slimeEnemy;
            }
            else if (instance is MagicianEnemy magician)
            {
                magician.Initialize(config, _experience, _coin, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer, points,
                    _isMoveRandomPoints, _mainBuildingPoint, EnemyTypes.Magician, _player, _projectile);

                return magician;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}
