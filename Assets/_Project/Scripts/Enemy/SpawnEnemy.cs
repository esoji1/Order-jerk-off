using _Project.Core.Interface;
using _Project.Enemy.Behaviors;
using _Project.Enemy.Types;
using _Project.SelectionGags;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Enemy
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.Enemys _enemys;
        [SerializeField] private int _maxEnemy;
        [SerializeField] private float _betweenSpawn;
        [SerializeField] private int _amountExperienceDropped;
        [SerializeField] private List<Enemy.Behaviors.Enemy> _enemy;
        [SerializeField] private EnemyFactoryBootstrap _enemyFactoryBootstrap;
        [SerializeField] private Experience _experiencePrefab;

        private Transform[] _points;

        private SpawnExperience _spawnExperience;

        private BattleZone _battleZone;
        private float _nextSpawnTime;
        private bool _isSpawning;

        public bool IsSpawning => _isSpawning;

        private void Start()
        {
            _isSpawning = true;
            _spawnExperience = new SpawnExperience(_experiencePrefab, _amountExperienceDropped);
        }

        private void Update()
        {
            Spawn();
        }

        public void Initialize(BattleZone battleZone, EnemyFactoryBootstrap enemyFactoryBootstrap, Transform[] points)
        {
            _battleZone = battleZone;
            if (enemyFactoryBootstrap != null)
                _enemyFactoryBootstrap = enemyFactoryBootstrap;
            _points = points;
        }

        public void DisableSpawner()
        {
            _isSpawning = false;
            _spawnExperience.Spawn(transform);
            Destroy(gameObject);
        }

        private void Spawn()
        {
            if (_battleZone.IsEnterZone && _isSpawning)
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
            Enemy.Behaviors.Enemy enemy = _enemys.GetEnemys[Random.Range(0, _enemys.GetEnemys.Count)];
            Enemy.Behaviors.Enemy newEnemy = _enemyFactoryBootstrap.EnemyFactory.Get(enemy.EnemyType, transform.position, _points);

            if (newEnemy != null)
            {
                _enemy.Add(newEnemy);
                IInitializeBattleZone initializeBattleZone = newEnemy.GetComponent<IInitializeBattleZone>();
                if (initializeBattleZone != null)
                    initializeBattleZone.Initialize(_battleZone);
            }
        }

        private void ClearEnemies()
        {
            foreach (Enemy.Behaviors.Enemy enemy in _enemy)
            {
                if (enemy != null && enemy.gameObject != null)
                {
                    enemy.DieHp();
                    Destroy(enemy.gameObject);
                }
            }

            _enemy.Clear();
        }

        private void OnDestroy()
        {
            ClearEnemies();
        }
    }
}