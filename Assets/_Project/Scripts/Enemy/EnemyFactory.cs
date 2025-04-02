using Assets._Project.Scripts.Enemy.Enemys;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core.HealthSystem;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class EnemyFactory
    {
        private EnemyConfig _commonEnemyConfig, _heavyCommonConfig;
        private BattleZone _battleZone;
        private SelectionGags.Experience _experience;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _dynamic;
        private LayerMask _layer;
        private WeaponFactoryBootstrap _weaponFactoryBootstrap;

        public EnemyFactory(EnemyConfig commonEnemy, EnemyConfig heavyCommonConfig, BattleZone battleZone, SelectionGags.Experience experience, HealthInfo healthInfoPrefab, 
            HealthView healthViewPrefab, Canvas dynamic, LayerMask layer, WeaponFactoryBootstrap weaponFactoryBootstrap)
        {
            _commonEnemyConfig = commonEnemy;
            _heavyCommonConfig = heavyCommonConfig;
            _battleZone = battleZone;
            _experience = experience;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _dynamic = dynamic;
            _layer = layer;
            _weaponFactoryBootstrap = weaponFactoryBootstrap;
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
                case EnemyTypes.CommonEnemy:
                    return _commonEnemyConfig;

                case EnemyTypes.HeavyCommonEnemy:
                    return _heavyCommonConfig;

                default:
                    throw new ArgumentException(nameof(types));
            }
        }

        private Enemy InitializeObject(Enemy instance, EnemyConfig config)
        {
            if (instance is CommonEnemy || instance is HeavyCommonEnemy)
            {
                instance.Initialize(config, _battleZone, _experience, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer, _weaponFactoryBootstrap);

                return instance;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}
