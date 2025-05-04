using _Project.ConstructionBuildings.Buildings;
using _Project.Core;
using _Project.Core.HealthSystem;
using _Project.Core.Interface;
using _Project.Enemy.MovePoints;
using _Project.ScriptableObjects.Configs;
using _Project.SelectionGags;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Enemy.Enemys
{
    [RequireComponent(typeof(RadiusMovementTrigger), typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public abstract class Enemy : MonoBehaviour, IDamage, IOnDamage
    {
        private EnemyConfig _config;
        private Experience _prefabExperience;
        private Coin _prefabCoin;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _dynamic;
        private LayerMask _layer;
        private List<Transform> _points;
        private bool _isMoveRandomPoints;
        private Transform _mainBuildingPoint;

        private PointExperience _pointExperience;
        private PointHealth _pointHealth;
        private PointCoin _pointCoin;
        private RadiusMovementTrigger _radiusMovementTrigger;
        private NavMeshAgent _agent;
        private EnemyView _enemyView;
        private BoxCollider2D _boxCollider2D;

        private Health _health;
        private SpawnExperience _spawnExperience;
        private SpawnCoin _spawnCoin;
        private HealthInfo _healthInfo;
        private HealthView _healthView;
        private RandomMovePoints _movePointsRandom;
        private MoveToPoint _moveToPoint;

        private Vector3 _previousPosition;
        private Vector3 _smoothedDirection;
        private Coroutine _coroutine;
        private bool _isDie;
        private bool _isAttack;
        private Collider2D _targetDamage;

        public event Action<int> OnDamage;
        public event Action<Enemy> OnEnemyDie;

        public PointHealth PointHealth => _pointHealth;
        public EnemyConfig Config => _config;
        public LayerMask LayerMask => _layer;
        public Health Health => _health;
        public EnemyView EnemyView => _enemyView;

        private void Update()
        {
            if (_isDie)
                return;

            _healthView.FollowTargetHealth();

            Move();
        }

        public virtual void Initialize(EnemyConfig config, SelectionGags.Experience prefabExperience, SelectionGags.Coin prefabCoin,
            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas dynamic, LayerMask layer, List<Transform> points, bool isMoveRandomPoints, Transform mainBuildingPoint)
        {
            ExtractComponents();

            _config = config;
            _prefabExperience = prefabExperience;
            _prefabCoin = prefabCoin;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _dynamic = dynamic;
            _layer = layer;
            _points = points;
            _isMoveRandomPoints = isMoveRandomPoints;
            _mainBuildingPoint = mainBuildingPoint;

            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.speed = _config.Speed;

            _movePointsRandom.Initialize(_points, _agent);
            _moveToPoint.Initialize(_mainBuildingPoint, _agent);

            _health = new Health(_config.Health);
            _spawnExperience = new SpawnExperience(_prefabExperience, _config.AmountExperienceDropped);
            _spawnCoin = new SpawnCoin(_prefabCoin, _config.AmountGoldDropped);

            _enemyView.Initialize();

            _healthInfo = Instantiate(_healthInfoPrefab, transform.position, Quaternion.identity);
            _healthInfo.Initialize(_dynamic);
            _healthView = Instantiate(_healthViewPrefab, transform.position, Quaternion.identity);
            _healthView.Initialize(this, _config.Health, _healthInfo, null);

            _radiusMovementTrigger.Initialize(transform, layer, transform, _config.Speed);

            _health.OnDie += Die;
        }

        public void Damage(int damage)
        {
            _health.TakeDamage(damage);
            OnDamage?.Invoke(damage);
        }

        public void Die()
        {
            _spawnExperience.Spawn(_pointExperience.transform);
            _spawnCoin.Spawn(_pointCoin.transform);
            _isDie = true;
            _enemyView.StartDie();
            _boxCollider2D.enabled = false;
            _agent.isStopped = true;
            OnEnemyDie?.Invoke(this);

            _health.OnDie -= Die;

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

        public void StartAttackIfNeeded()
        {
            if (_coroutine == null)
            {
                _isAttack = true;
                _coroutine = StartCoroutine(Attack());
            }
        }

        private void StopAttackIfNeeded()
        {
            if (_coroutine != null)
            {
                _isAttack = false;
                _enemyView.StopAttack();
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator Attack()
        {
            while (_isAttack && _isDie == false)
            {
                if (CheckAttackHitRadius())
                {
                    _enemyView.StartAttack();

                    float attackAnimationTime = _enemyView.Animator.GetCurrentAnimatorStateInfo(0).length;
                    yield return new WaitForSeconds(attackAnimationTime);

                    if (CheckAttackHitRadius())
                        DamageTarget();

                    _enemyView.StopAttack();

                    yield return new WaitForSeconds(1);
                }
                else
                {
                    StopAttackIfNeeded();
                }
            }
        }

        private bool CheckAttackHitRadius()
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, _config.AttackRadius, _layer);

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out Player.Player _) || collider.TryGetComponent(out BaseBuilding _))
                {
                    _targetDamage = collider;
                    return true;
                }
            }

            return false;
        }

        private void DamageTarget()
        {
            if (_targetDamage == null)
                return;

            _targetDamage.GetComponent<IDamage>().Damage(_config.Damage);
        }

        private void Move()
        {
            Vector3 currentDirection = (transform.position - _previousPosition).normalized;

            _previousPosition = transform.position;

            _smoothedDirection = Vector3.Lerp(_smoothedDirection, currentDirection, Time.deltaTime * 10f);

            _enemyView.UpdateRunX(_smoothedDirection.x);
            _enemyView.UpdateRunY(_smoothedDirection.y);

            if (CheckAttackHitRadius())
            {
                StartAttackIfNeeded();
            }

            if (_radiusMovementTrigger.MoveToTarget(_config.AttackRadius, _config.VisibilityRadius))
            {
                _agent.isStopped = true;
                return;
            }

            if (_isMoveRandomPoints)
            {
                _movePointsRandom.MovePoints();
            }

            if(_isMoveRandomPoints == false)
            {
                _moveToPoint.MovePoints();
            }
        }

        private void ExtractComponents()
        {
            _pointExperience = GetComponentInChildren<PointExperience>();
            _pointCoin = GetComponentInChildren<PointCoin>();
            _pointHealth = GetComponentInChildren<PointHealth>();
            _radiusMovementTrigger = GetComponent<RadiusMovementTrigger>();
            _agent = GetComponent<NavMeshAgent>();
            _enemyView = GetComponentInChildren<EnemyView>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _movePointsRandom = GetComponent<RandomMovePoints>();
            _moveToPoint = GetComponent<MoveToPoint>();
        }
    }
}