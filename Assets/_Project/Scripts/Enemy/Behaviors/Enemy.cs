using _Project.Core;
using _Project.Core.HealthSystem;
using _Project.Core.Interface;
using _Project.Core.Points;
using _Project.Enemy.Breakers;
using _Project.Enemy.Types;
using _Project.Quests.KillQuest;
using _Project.ScriptableObjects;
using _Project.SelectionGags;
using _Project.SelectionGags.Artefacts;
using System;
using UnityEngine;

namespace _Project.Enemy.Behaviors
{
    [RequireComponent(typeof(UpdateEnemyView))]
    public class Enemy : MonoBehaviour, IDamage, IOnDamage
    {
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _uiDynamic;
        private EnemyConfig _enemyConfig;
        private Enum _enemyTypes;
        private Player.Player _player;
        private GivesData _givesData;

        private Health _health;
        private SpawnExperience _spawnExperience;
        private SpawnCoin _spawnCoin;
        private SpawnArtefact _spawnArtefact;

        private PointHealth _pointHealth;
        private EnemyView _enemyView;
        private AgentMovement _agentMovement;
        private BoxCollider2D _boxCollider2D;
        private PointExperience _pointExperience;
        private PointCoin _pointCoin;
        private PointArtefact _pointArtefact;
        private ReasonCompleteStopAttack _reasonCompleteStopAttack;
        private ReasonCompleteStopMovement _reasonCompleteStopMovement;

        private HealthInfo _healthInfo;
        private HealthView _healthView;
        private bool _isDie;
        private bool _isSleeping;
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
            _player.OnInvisible -= StopChase;
        }

        public void Initialize(HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas uiDynamic, EnemyConfig enemyConfig,
            Enum enemyTypes, Experience _experiencePrefab, Coin coinPrefab, Player.Player player, GivesData givesData)
        {
            ExtractComponents();

            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _uiDynamic = uiDynamic;
            _enemyConfig = enemyConfig;
            _enemyTypes = enemyTypes;
            _player = player;
            _givesData = givesData;

            _health = new Health(_enemyConfig.Health);
            _spawnExperience = new SpawnExperience(_experiencePrefab, _enemyConfig.AmountExperienceDropped);
            _spawnCoin = new SpawnCoin(coinPrefab, _enemyConfig.AmountGoldDropped);
            _spawnArtefact = new SpawnArtefact(givesData);

            _healthInfo = Instantiate(_healthInfoPrefab, transform.position, Quaternion.identity);
            _healthInfo.Initialize(_uiDynamic);
            _healthView = Instantiate(_healthViewPrefab, transform.position, Quaternion.identity);
            _healthView.Initialize(this, _enemyConfig.Health, _healthInfo, null);

            _health.OnDie += Die;
            _player.OnInvisible += StopChase;
        }

        public void Damage(int damage)
        {
            _health.TakeDamage(damage);
            OnDamage?.Invoke(damage);
        }

        public void SetSleeps(bool value)
        {
            _isSleeping = value;

            if (_isSleeping)
            {
                _reasonCompleteStopMovement.Emit(MovementBreakReasonType.Manual);
                _reasonCompleteStopAttack.Emit(BreakerEnemyType.Manual);
            }
            else if (_isSleeping == false)
            {
                _reasonCompleteStopMovement.Emit(MovementBreakReasonType.Chase);
                _reasonCompleteStopAttack.EmitStarting(BreakerEnemyType.Manual);
            }
        }

        public void Die()
        {
            _spawnExperience.Spawn(_pointExperience.transform);
            _spawnCoin.Spawn(_pointCoin.transform);
            _spawnArtefact.Spawn(_pointArtefact.transform, UnityEngine.Random.Range(0, _givesData.Gives.Count));
            _isDie = true;
            _reasonCompleteStopMovement.Emit(MovementBreakReasonType.Manual);
            _boxCollider2D.enabled = false;
            _reasonCompleteStopAttack.Emit(BreakerEnemyType.Manual);
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
            _reasonCompleteStopMovement = GetComponent<ReasonCompleteStopMovement>();
            _pointArtefact = GetComponentInChildren<PointArtefact>();
        }

        private void StopChase(bool value)
        {
            if (value)
            {
                _reasonCompleteStopMovement.Emit(MovementBreakReasonType.OnlyChase);
                _reasonCompleteStopMovement.Emit(MovementBreakReasonType.Patrol);
                _reasonCompleteStopAttack.Emit(BreakerEnemyType.Manual);
            }
            else if (value == false)
            {
                _reasonCompleteStopMovement.Emit(MovementBreakReasonType.Chase);
                _reasonCompleteStopAttack.EmitStarting(BreakerEnemyType.Manual);
            }
        }
    }
}