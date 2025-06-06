using _Project.Core;
using _Project.Core.HealthSystem;
using _Project.Core.Interface;
using _Project.Core.Points;
using _Project.Quests.KillQuest;
using _Project.SelectionGags;
using System;
using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(UpdateEnemyView))]
    public class Enemy : MonoBehaviour, IDamage, IOnDamage
    {
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _uiDynamic;
        private EnemyConfig _enemyConfig;
        private EnemyTypes _enemyTypes;

        private Health _health;
        private SpawnExperience _spawnExperience;
        private SpawnCoin _spawnCoin;

        private PointHealth _pointHealth;
        private EnemyView _enemyView;
        private AgentMovement _agentMovement;
        private BoxCollider2D _boxCollider2D;
        private PointExperience _pointExperience;
        private PointCoin _pointCoin;
        private ReasonCompleteStopAttack _reasonCompleteStopAttack;

        private HealthInfo _healthInfo;
        private HealthView _healthView;
        private bool _isDie;

        public PointHealth PointHealth => _pointHealth;

        public event Action<int> OnDamage;
        public event Action<Enemy> OnEnemyDie;

        private void Update()
        {
            if (_isDie)
                return;

            _healthView.FollowTargetHealth();
        }

        private void OnDestroy()
        {
            _health.OnDie -= Die;
        }

        public void Initialize(HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas uiDynamic, EnemyConfig enemyConfig,
            EnemyTypes enemyTypes, Experience _experiencePrefab, Coin coinPrefab)
        {
            ExtractComponents();

             _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _uiDynamic = uiDynamic;
            _enemyConfig = enemyConfig;
            _enemyTypes = enemyTypes;

            _health = new Health(_enemyConfig.Health);
            _spawnExperience = new SpawnExperience(_experiencePrefab, _enemyConfig.AmountExperienceDropped);
            _spawnCoin = new SpawnCoin(coinPrefab, _enemyConfig.AmountGoldDropped);

            _healthInfo = Instantiate(_healthInfoPrefab, transform.position, Quaternion.identity);
            _healthInfo.Initialize(_uiDynamic);
            _healthView = Instantiate(_healthViewPrefab, transform.position, Quaternion.identity);
            _healthView.Initialize(this, _enemyConfig.Health, _healthInfo, null);

            _health.OnDie += Die;
        }

        public void Damage(int damage)
        {
            _health.TakeDamage(damage);
            OnDamage?.Invoke(damage);
        }

        public void SetSleeps(bool value)
        {
        }

        public void Die()
        {
            _spawnExperience.Spawn(_pointExperience.transform);
            _spawnCoin.Spawn(_pointCoin.transform);
            _isDie = true;
            _agentMovement.Agent.isStopped = true;
            _boxCollider2D.enabled = false;
            _reasonCompleteStopAttack.Emit(ReasonCompleteStopAttackType.Manual);
            _enemyView.StartDie();
            OnEnemyDie?.Invoke(this);
            EnemyCounterQuest.Instance.AddKill(_enemyTypes);

            DieHp();
            Destroy(gameObject, 5f);
        }

        public void DieHp()
        {
            if (_healthInfo != null || _healthInfo != null || _healthView != null)
            {
                Destroy(_healthInfo.InstantiatedHealthBar.gameObject);
                Destroy(_healthInfo.gameObject);
                Destroy(_healthView.gameObject);
            }
        }

        private void ExtractComponents()
        {
            _pointHealth = GetComponentInChildren<PointHealth>();
            _enemyView = GetComponentInChildren<EnemyView>();
            _agentMovement = GetComponent<AgentMovement>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _pointExperience = GetComponentInChildren<PointExperience>();
            _pointCoin = GetComponentInChildren<PointCoin>();
            _reasonCompleteStopAttack = GetComponent<ReasonCompleteStopAttack>();
        }
    }
}