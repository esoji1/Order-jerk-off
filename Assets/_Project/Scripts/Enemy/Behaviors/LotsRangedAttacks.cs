using _Project.Core.Interface;
using _Project.Core.Projectile;
using _Project.Enemy.Breakers;
using _Project.Enemy.Types;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy.Behaviors
{
    [RequireComponent(typeof(ReasonCompleteStopAttack), typeof(AttackBreaker))]
    public class LotsRangedAttacks : MonoBehaviour, IInitializePlayer
    {
        [SerializeField] private ProjectileEnemy projectileEnemyPrefab;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackInterval;
        [SerializeField] private FieldOfViewAttack _fovViewAttack;
        [SerializeField] private Transform[] _pointsAttack;

        private Player.Player _player;

        private SpawnProjectile _spawnProjectile;

        private EnemyView.BaseEnemyView _enemyView;
        private ReasonCompleteStopAttack _reasonCompleteStopAttack;
        private AttackBreaker _attackBreaker;

        private Coroutine _coroutine;
        private bool _isAttack;
        private ProjectileEnemy _projectileEnemy;

        private void Awake()
        {
            ExtractComponents();
            _enemyView.Initialize();

            _spawnProjectile = new SpawnProjectile();

            _fovViewAttack.OnAttack += StartAttack;
            _fovViewAttack.OnStopAttack += StopAttack;
            _reasonCompleteStopAttack.BreakRequested += StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested += StartAttackCompletely;
            _attackBreaker.BreakRequested += TryBreakRangedAttack;
        }

        private void OnDestroy()
        {
            if (_projectileEnemy != null)
                Destroy(_projectileEnemy.gameObject);

            _fovViewAttack.OnAttack -= StartAttack;
            _fovViewAttack.OnStopAttack -= StopAttack;
            _reasonCompleteStopAttack.BreakRequested -= StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested -= StartAttackCompletely;
            _attackBreaker.BreakRequested -= TryBreakRangedAttack;
        }

        public void Initialize(Player.Player player) => _player = player;

        private void ExtractComponents()
        {
            _enemyView = GetComponentInChildren<EnemyView.BaseEnemyView>();
            _reasonCompleteStopAttack = GetComponent<ReasonCompleteStopAttack>();
            _attackBreaker = GetComponent<AttackBreaker>();
        }

        private void StartAttack(Transform target)
        {
            StartCoroutine();
        }

        private void StopAttack(Transform target)
        {
            StopCoroutine();
        }

        private void StartCoroutine()
        {
            if (_coroutine == null && _isAttack == false)
                _coroutine = StartCoroutine(Attack());
        }

        private void StopCoroutine()
        {
            if (_coroutine != null)
            {
                _enemyView.StopRangeAttack();
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(_attackInterval);

                foreach (Transform transform in _pointsAttack)
                {
                    _enemyView.StartRangeAttack();

                    Vector2 direction = (_player.transform.position - transform.position).normalized;
                    GameObject bulletGameObject = _spawnProjectile.ProjectileSpawnPoint(projectileEnemyPrefab.gameObject, direction, transform);
                    _projectileEnemy = bulletGameObject.GetComponent<ProjectileEnemy>();
                    _projectileEnemy.Initialize(direction, _projectileEnemy, _damage);
                }
            }
        }

        private void StopAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.LotsRangedAttacks)
            {
                _isAttack = true;
                StopAttack(null);
            }
        }

        private void StartAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.LotsRangedAttacks)
            {
                StopCoroutine();

                _isAttack = false;

                if (_fovViewAttack.CheckPlayerInRadius())
                    StartCoroutine();
            }
        }

        private void TryBreakRangedAttack(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.LotsRangedAttacks)
            {
                StopAttack(null);
            }
            else if (type is BreakerEnemyType.LotsRangedAttacks)
            {
                StartAttack(null);
            }
        }
    }
}