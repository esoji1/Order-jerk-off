using Assets._Project.Scripts.ScriptableObjects.Configs;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class EnemyFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _commonEnemyConfig;

        private EnemyFactory _enemyFactory;

        public bool IsSpawn;

        private void Awake()
        {
            _enemyFactory  = new EnemyFactory(_commonEnemyConfig);
        }

        private void Update()
        {
            if(IsSpawn == false)
            {
                _enemyFactory.Get(EnemyTypes.CommonEnemy, transform.position);
                IsSpawn = true;
            }
        }
    }
}
