//using _Project.Core;
//using _Project.Core.HealthSystem;
//using _Project.Core.Interface;
//using _Project.Core.Points;
//using _Project.Enemy.Attakcs;
//using _Project.Enemy.MovePoints;
//using _Project.Quests.KillQuest;
//using _Project.ScriptableObjects.Configs;
//using _Project.SelectionGags;
//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//namespace _Project.Enemy.Enemys
//{
//    [RequireComponent(typeof(RadiusMovementTrigger), typeof(Rigidbody2D), typeof(BoxCollider2D))]
//    [RequireComponent(typeof(AttackEnemyFactory))]
//    public abstract class Enemy : MonoBehaviour, IDamage, IOnDamage
//    {
//        private ScriptableObjects.Configs.EnemyConfig _config;
//        private Experience _prefabExperience;
//        private Coin _prefabCoin;
//        private HealthInfo _healthInfoPrefab;
//        private HealthView _healthViewPrefab;
//        private Canvas _dynamic;
//        private LayerMask _layer;
//        private List<Transform> _points;
//        private bool _isMoveRandomPoints;
//        private Transform _mainBuildingPoint;
//        private Player.Player _player;

//        private PointExperience _pointExperience;
//        private PointHealth _pointHealth;
//        private PointCoin _pointCoin;
//        private RadiusMovementTrigger _radiusMovementTrigger;
//        private NavMeshAgent _agent;
//        private EnemyView _enemyView;
//        private BoxCollider2D _boxCollider2D;

//        private Health _health;
//        private SpawnExperience _spawnExperience;
//        private SpawnCoin _spawnCoin;
//        private HealthInfo _healthInfo;
//        private HealthView _healthView;
//        private RandomMovePoints _movePointsRandom;
//        private MoveToTarget _moveToPoint;

//        private bool _isDie;
//        private bool _isSleeps;
//        private EnemyType _enemyTypes;

//        public event Action<int> OnDamage;
//        public event Action<Enemy> OnEnemyDie;

//        public PointHealth PointHealth => _pointHealth;
//        public ScriptableObjects.Configs.EnemyConfig Config => _config;
//        public LayerMask LayerMask => _layer;
//        public Health Health => _health;
//        public EnemyView EnemyView => _enemyView;
//        public bool IsDie => _isDie;
//        public HealthView HealthView => _healthView;
//        public LayerMask Layer => _layer;
//        public RadiusMovementTrigger RadiusMovementTrigger => _radiusMovementTrigger;
//        public NavMeshAgent Agent => _agent;
//        public bool IsMoveRandomPoints => _isMoveRandomPoints;
//        public RandomMovePoints RandomMovePoints => _movePointsRandom;
//        public MoveToTarget MoveToTarget => _moveToPoint;
//        public Player.Player Player => _player; 
//        public bool IsSleeps => _isSleeps;

//        public virtual void Initialize(ScriptableObjects.Configs.EnemyConfig config, Experience prefabExperience, Coin prefabCoin, HealthInfo healthInfoPrefab, 
//            HealthView healthViewPrefab, Canvas dynamic, LayerMask layer, List<Transform> points, bool isMoveRandomPoints, 
//            Transform mainBuildingPoint, EnemyType enemyTypes, Player.Player player)
//        {
//            ExtractComponents();

//            _config = config;
//            _prefabExperience = prefabExperience;
//            _prefabCoin = prefabCoin;
//            _healthInfoPrefab = healthInfoPrefab;
//            _healthViewPrefab = healthViewPrefab;
//            _dynamic = dynamic;
//            _layer = layer;
//            _points = points;
//            _isMoveRandomPoints = isMoveRandomPoints;
//            _mainBuildingPoint = mainBuildingPoint;
//            _enemyTypes = enemyTypes;
//            _player = player;

//            _agent.updateRotation = false;
//            _agent.updateUpAxis = false;
//            _agent.speed = _config.Speed;

//            _movePointsRandom.Initialize(_points, _agent);
//            _moveToPoint.Initialize(_mainBuildingPoint, _agent);

//            _health = new Health(_config.Health);
//            _spawnExperience = new SpawnExperience(_prefabExperience, _config.AmountExperienceDropped);
//            _spawnCoin = new SpawnCoin(_prefabCoin, _config.AmountGoldDropped);

//            _enemyView.Initialize();

//            _healthInfo = Instantiate(_healthInfoPrefab, transform.position, Quaternion.identity);
//            _healthInfo.Initialize(_dynamic);
//            _healthView = Instantiate(_healthViewPrefab, transform.position, Quaternion.identity);
//            _healthView.Initialize(this, _config.Health, _healthInfo, null);

//            _radiusMovementTrigger.Initialize(transform, layer, transform, _config.Speed);

//            _health.OnDie += Die;
//        }

//        public void Damage(int damage)
//        {
//            _health.TakeDamage(damage);
//            OnDamage?.Invoke(damage);
//        }

//        public void Die()
//        {
//            _spawnExperience.Spawn(_pointExperience.transform);
//            _spawnCoin.Spawn(_pointCoin.transform);
//            _isDie = true;
//            _enemyView.StartDie();
//            _boxCollider2D.enabled = false;
//            _agent.isStopped = true;
//            OnEnemyDie?.Invoke(this);
//            EnemyCounterQuest.Instance.AddKill(_enemyTypes);

//            _health.OnDie -= Die;

//            DieHp();
//            Destroy(gameObject, 5f);
//        }

//        public void DieHp()
//        {
//            if (_healthInfo != null || _healthInfo != null || _healthView != null)
//            {
//                Destroy(_healthInfo.InstantiatedHealthBar.gameObject);
//                Destroy(_healthInfo.gameObject);
//                Destroy(_healthView.gameObject);
//            }
//        }

//        public void SetSleeps(bool value)
//        {
//            _isSleeps = value;
//        }

//        private void ExtractComponents()
//        {
//            _pointExperience = GetComponentInChildren<PointExperience>();
//            _pointCoin = GetComponentInChildren<PointCoin>();
//            _pointHealth = GetComponentInChildren<PointHealth>();
//            _radiusMovementTrigger = GetComponent<RadiusMovementTrigger>();
//            _agent = GetComponent<NavMeshAgent>();
//            _enemyView = GetComponentInChildren<EnemyView>();
//            _boxCollider2D = GetComponent<BoxCollider2D>();
//            _movePointsRandom = GetComponent<RandomMovePoints>();
//            _moveToPoint = GetComponent<MoveToTarget>();
//        }
//    }
//}