using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Core.HealthSystem;
using Assets._Project.Scripts.Core.Interface;
using Assets._Project.Scripts.Core.Spawns;
using Assets._Project.Scripts.Enemy.MovePoints;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Core.HealthSystem;
using Assets._Project.Sctipts.Core.Spawns;
using System;
using System.Collections.Generic;
using UnityEditor.Splines;
using UnityEngine;
using UnityEngine.AI;

namespace Assets._Project.Scripts.Enemy
{
    [RequireComponent(typeof(RadiusMovementTrigger), typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public abstract class Enemy : MonoBehaviour, IDamage, IOnDamage
    {
        protected WeaponFactoryBootstrap WeaponFactoryBootstrap;
        protected SetWeaponPoint SetWeaponPoint;
        protected PointRotation PointRotation;

        private EnemyConfig _config;
        private BattleZone _battleZone;
        private SelectionGags.Experience _prefabExperience;
        private SelectionGags.Coin _prefabCoin;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _dynamic;
        private LayerMask _layer;
        private List<Transform> _points;

        private PointExperience _pointExperience;
        private PointHealth _pointHealth;
        private PointCoin _pointCoin;
        private RadiusMovementTrigger _radiusMovementTrigger;
        private NavMeshAgent _agent;
        private EnemyView _enemyView;
        private Rigidbody2D _rigidbody2D;

        private Health _health;
        private SpawnExperience _spawnExperience;
        private SpawnCoin _spawnCoin;
        private HealthInfo _healthInfo;
        private HealthView _healthView;
        private IMovePoints _movePoints;

        private Vector3 _previousPosition;
        private Vector3 _smoothedDirection;

        public event Action<int> OnDamage;

        public PointHealth PointHealth => _pointHealth;
        public EnemyConfig Config => _config;
        public LayerMask LayerMask => _layer;

        private void Update()
        {
            Vector3 currentDirection = (transform.position - _previousPosition).normalized;

            _previousPosition = transform.position;

            _smoothedDirection = Vector3.Lerp(_smoothedDirection, currentDirection, Time.deltaTime * 10f);

            _enemyView.Animator.SetFloat("Horizontal", _smoothedDirection.x);
            _enemyView.Animator.SetFloat("Vertical", _smoothedDirection.y);

            _healthView.FollowTargetHealth();

            if (_radiusMovementTrigger.MoveToTarget(_config.AttackRadius, _config.VisibilityRadius))
            {
                _agent.isStopped = true;
                return;
            }

            _movePoints.MovePoints();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy _) &&
                _radiusMovementTrigger.MoveToTarget(_config.AttackRadius, _config.VisibilityRadius) == false)
            {
                _agent.ResetPath();
            }
        }

        public virtual void Initialize(EnemyConfig config, SelectionGags.Experience prefabExperience, SelectionGags.Coin prefabCoin,
            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas dynamic, LayerMask layer, List<Transform> points)
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

            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.speed = _config.Speed;

            _movePoints = new RandomMovePoints(_points, _agent);

            _health = new Health(_config.Health);
            _spawnExperience = new SpawnExperience(_prefabExperience, _config.AmountExperienceDropped);
            _spawnCoin = new SpawnCoin(_prefabCoin, _config.AmountGoldDropped);
            SetWeaponPoint = new SetWeaponPoint();

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

        private void ExtractComponents()
        {
            _pointExperience = GetComponentInChildren<PointExperience>();
            _pointCoin = GetComponentInChildren<PointCoin>();
            _pointHealth = GetComponentInChildren<PointHealth>();
            _radiusMovementTrigger = GetComponent<RadiusMovementTrigger>();
            PointRotation = GetComponentInChildren<PointRotation>();
            _agent = GetComponent<NavMeshAgent>();
            _enemyView = GetComponentInChildren<EnemyView>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}