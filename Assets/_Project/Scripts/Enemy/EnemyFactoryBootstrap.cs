using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core.HealthSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class EnemyFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _commonEnemyConfig, _heavyCommonConfig;
        [SerializeField] private BattleZone _battleZone;
        [SerializeField] private SelectionGags.Experience _experience;
        [SerializeField] private HealthInfo _healthInfoPrefab;
        [SerializeField] private HealthView _healthViewPrefab;
        [SerializeField] private Canvas _dynamic;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private WeaponFactoryBootstrap _weaponFactoryBootstrap;

        private EnemyFactory _enemyFactory;

        public bool IsSpawn;
        public bool IsSpawn2;

        public EnemyFactory EnemyFactory => _enemyFactory;

        private void Awake()
        {
            _enemyFactory  = new EnemyFactory(_commonEnemyConfig, _heavyCommonConfig, _battleZone, _experience, _healthInfoPrefab, _healthViewPrefab, _dynamic, _layer,
                _weaponFactoryBootstrap);
        }

        private void Update()
        {
            if(IsSpawn == false)
            {
                _enemyFactory.Get(EnemyTypes.CommonEnemy, transform.position);
                IsSpawn = true;
            }  if(IsSpawn2 == false)
            {
                _enemyFactory.Get(EnemyTypes.HeavyCommonEnemy, transform.position);
                IsSpawn2 = true;
            }
        }
    }
}
