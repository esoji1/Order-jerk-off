using Assets._Project.Scripts.ScriptableObjects.Configs;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class EnemyFactory
    {
        private EnemyConfig _commonEnemyConfig;

        public EnemyFactory(EnemyConfig commonEnemy)
        {
            _commonEnemyConfig = commonEnemy;
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

                default:
                    throw new ArgumentException(nameof(types));
            }
        }

        private Enemy InitializeObject(Enemy instance, EnemyConfig config)
        {
            if (instance is CommonEnemy)
            {
                instance.Initialize(config);

                return instance;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}
