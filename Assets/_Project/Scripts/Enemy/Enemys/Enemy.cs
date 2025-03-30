using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Core.HealthSystem;
using Assets._Project.Scripts.Core.Interface;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Core.HealthSystem;
using Assets._Project.Sctipts.Core.Spawns;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    [RequireComponent(typeof(AttackEnemyFactory))]
    public abstract class Enemy : MonoBehaviour, IDamage, IOnDamage
    {
        private EnemyConfig _config;
        private BattleZone _battleZone;
        private SelectionGags.Experience _prefab;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _dynamic;
        private LayerMask _layer;

        private PointExperience _pointExperience;
        private PointHealth _pointHealth;
        private PointCoin _pointCoin;
        private PointAttack _pointAttack;
        private AttackEnemyFactory _attackEnemyFactory;

        private Health _health;
        private SpawnExperience _spawnExperience;
        private HealthInfo _healthInfo;
        private HealthView _healthView;

        public event Action<int> OnDamage;

        public BattleZone BattleZone => _battleZone;
        public PointHealth PointHealth => _pointHealth;
        public EnemyConfig Config => _config;
        public PointAttack PointAttack => _pointAttack;
        public LayerMask LayerMask => _layer;

        public virtual void Initialize(EnemyConfig config, BattleZone battleZone, SelectionGags.Experience prefab, HealthInfo healthInfoPrefab,
            HealthView healthViewPrefab, Canvas dynamic, LayerMask layer)
        {
            ExtractComponents();

            _config = config;
            _battleZone = battleZone;
            _prefab = prefab;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _dynamic = dynamic;
            _layer = layer;
            _health = new Health(_config.Health);
            _spawnExperience = new SpawnExperience(_prefab, _config.AmountExperienceDropped);

            _healthInfo = Instantiate(_healthInfoPrefab, transform.position, Quaternion.identity);
            _healthInfo.Initialize(_dynamic);
            _healthView = Instantiate(_healthViewPrefab, transform.position, Quaternion.identity);
            _healthView.Initialize(this, _config.Health, _healthInfo, null);

            _health.OnDie += Die;
        }

        private void Update()
        {
            _healthView.FollowTargetHealth();
            _attackEnemyFactory.BaseEnemy.Attack();
        }

        public void Damage(int damage)
        {
            _health.TakeDamage(damage);
            OnDamage?.Invoke(damage);
        }

        private void ExtractComponents()
        {
            _attackEnemyFactory = GetComponentInChildren<AttackEnemyFactory>();
            _pointExperience = GetComponentInChildren<PointExperience>();
            _pointCoin = GetComponentInChildren<PointCoin>();
            _pointHealth = GetComponentInChildren<PointHealth>();
            _pointAttack = GetComponentInChildren<PointAttack>();
        }

        public void Die()
        {
            _spawnExperience.Spawn(_pointExperience.transform);

            _health.OnDie -= Die;
            Destroy(_healthInfo.InstantiatedHealthBar.gameObject);
            Destroy(_healthInfo.gameObject);
            Destroy(_healthView.gameObject);
            Destroy(gameObject);
        }

        public void DieHp()
        {
            Destroy(_healthInfo.InstantiatedHealthBar.gameObject);
            Destroy(_healthInfo.gameObject);
            Destroy(_healthView.gameObject);
        }
    }
}