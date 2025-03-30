using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Core.HealthSystem;
using Assets._Project.Scripts.Core.Interface;
using Assets._Project.Scripts.Enemy;
using Assets._Project.Scripts.ScriptableObjects;
using Assets._Project.Sctipts.Core.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour, IDamage, IOnDamage
{
    [SerializeField] private Enemys _enemys;
    [SerializeField] private int _maxEnemy;
    [SerializeField] private float _betweenSpawn;
    [SerializeField] private List<Enemy> _enemy;
    [SerializeField] private EnemyFactoryBootstrap _enemyFactoryBootstrap;
    [SerializeField] private HealthInfo _healthInfoPrefab;
    [SerializeField] private HealthView _healthViewPrefab;
    [SerializeField] private Canvas _dynamic;

    private Health _health;

    private HealthView _healthView;
    private HealthInfo _healthInfo;
    private PointHealth _pointHealth;
    private BattleZone _battleZone;
    private Coroutine _coroutine;
    private float _nextSpawnTime;

    public PointHealth PointHealth => _pointHealth;

    public event System.Action<int> OnDamage;

    private void Start()
    {
        _health = new Health(30);
        _pointHealth = GetComponentInChildren<PointHealth>();

        _healthInfo = Instantiate(_healthInfoPrefab, transform.position, Quaternion.identity);
        _healthInfo.Initialize(_dynamic);
        _healthView = Instantiate(_healthViewPrefab, transform.position, Quaternion.identity);
        _healthView.Initialize(this, _health.HealthValue, _healthInfo, null);

        _health.OnDie += Die;
    }

    private void Update()
    {
        _healthView.FollowTargetHealth();

        if (_battleZone.IsEnterZone)
        {
            if (_nextSpawnTime <= 0)
                _nextSpawnTime = Time.time + 1f; 

            if (Time.time >= _nextSpawnTime)
            {
                _enemy.RemoveAll(e => e == null || e.gameObject == null);
                if (_enemy.Count < _maxEnemy)
                {
                    SpawnEnemyLogic();
                    _nextSpawnTime = Time.time + _betweenSpawn;
                }
                else
                {
                    _nextSpawnTime = Time.time + 0.5f;
                }
            }
        }
        else
        {
            ClearEnemies();
            _nextSpawnTime = 0f;
        }
    }

    private void SpawnEnemyLogic()
    {
        EnemyTypes enemyType = (EnemyTypes)Random.Range(0, _enemys.GetEnemys.Count);
        Enemy newEnemy = _enemyFactoryBootstrap.EnemyFactory.Get(enemyType, transform.position);
        if (newEnemy != null)
            _enemy.Add(newEnemy);
    }

    private void ClearEnemies()
    {
        foreach (var enemy in _enemy)
        {
            if (enemy != null && enemy.gameObject != null)
            {
                enemy.DieHp();
                Destroy(enemy.gameObject);
            }
        }
        _enemy.Clear();
    }

    public void Initialize(BattleZone battleZone)
    {
        _battleZone = battleZone;
    }

    public void Damage(int damage)
    {
        _health.TakeDamage(damage);
        OnDamage?.Invoke(damage);
    }

    private void Die()
    {
        _health.OnDie -= Die;
        Destroy(_healthInfo.InstantiatedHealthBar.gameObject);
        Destroy(_healthInfo.gameObject);
        Destroy(_healthView.gameObject);
        Destroy(gameObject);
    }

}
