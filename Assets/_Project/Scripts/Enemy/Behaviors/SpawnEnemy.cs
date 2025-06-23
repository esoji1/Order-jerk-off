using _Project.Core.Interface;
using _Project.Enemy.Breakers;
using _Project.Enemy.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Enemy.Behaviors
{
    [RequireComponent(typeof(ReasonCompleteStopAttack))]
    public class SpawnEnemy : MonoBehaviour, IInitializeEnemyFactory, IInitializeBattleZone
    {
        [SerializeField] private EnemyType _spawnEnemyType;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private Transform[] _pointsSpawn;

        private EnemyFactory _enemyFactory;

        private ReasonCompleteStopAttack _reasonCompleteStopAttack;
        private BattleZone _battleZone;

        private Coroutine _coroutine;
        private bool _isAttack;
        private List<Enemy> _enemy = new();

        private void Awake()
        {
            ExtractComponents();

            _reasonCompleteStopAttack.BreakRequested += StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested += StartAttackCompletely;

            StartCoroutine();
        }

        private void OnDestroy()
        {
            _reasonCompleteStopAttack.BreakRequested -= StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested -= StartAttackCompletely;
            _battleZone.OnEnterZone -= StartCoroutine;
            _battleZone.OnExitZone -= ClearEnemy;

            StopCorourine();
        }

        public void Initialize(EnemyFactory enemyFactory) => _enemyFactory = enemyFactory;

        public void Initialize(BattleZone battleZone)
        {
            _battleZone = battleZone;

            _battleZone.OnEnterZone += StartCoroutine;
            _battleZone.OnExitZone += ClearEnemy;
        }

        private void ExtractComponents()
        {
            _reasonCompleteStopAttack = GetComponent<ReasonCompleteStopAttack>();
        }

        private void StartAttack(Transform target)
        {
            StartCoroutine();
        }

        private void StopAttack(Transform target)
        {
            StopCorourine();
        }

        private void StartCoroutine()
        {
            if (_coroutine == null && _isAttack == false)
                _coroutine = StartCoroutine(Attack());
        }

        private void StopCorourine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);

                foreach (Transform point in _pointsSpawn)
                {
                    Enemy enemy = _enemyFactory.Get(_spawnEnemyType, point.position, null);
                    _enemy.Add(enemy);
                }
            }
        }

        private void ClearEnemy()
        {
            foreach (Enemy enemy in _enemy)
            {
                if (enemy != null && enemy.gameObject != null)
                {
                    enemy.DieHp();
                    Destroy(enemy.gameObject);
                }
            }

            _enemy.Clear();
        }

        private void StopAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.SpawnEnemy)
            {
                _isAttack = true;
                StopCorourine();
            }
        }

        private void StartAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.SpawnEnemy)
            {
                StopCorourine();

                _isAttack = false;

                StartCoroutine();
            }
        }
    }
}