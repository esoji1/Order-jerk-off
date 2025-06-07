using _Project.Weapon.Projectile;
using Assets._Project.Scripts.Enemy;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(ReasonCompleteStopAttack), typeof(AttackBreaker))]
    public class RangedAttack : MonoBehaviour, IInitializePlayer
    {
        [SerializeField] private ProjectileEnemy projectileEnemyPrefab;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackInterval;
        [SerializeField] private FieldOfViewAttack _fovViewAttack;

        private Player.Player _player;

        private SpawnProjectile _spawnProjectile;

        private EnemyView _enemyView;
        private ReasonCompleteStopAttack _reasonCompleteStopAttack;
        private AttackBreaker _attackBreaker;

        private Coroutine _coroutine;
        private bool _isAttack;

        private void Awake()
        {
            ExtractComponents();
            _enemyView.Initialize();

            _spawnProjectile = new SpawnProjectile();

            _fovViewAttack.OnPlayerAttack += StartAttack;
            _fovViewAttack.OnPlayerStopAttack += StopAttack;
            _reasonCompleteStopAttack.BreakRequested += StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested += StartAttackCompletely;
            _attackBreaker.BreakRequested += TryBreakRangedAttack;
        }

        private void OnDestroy()
        {
            _fovViewAttack.OnPlayerAttack -= StartAttack;
            _fovViewAttack.OnPlayerStopAttack -= StopAttack;
            _reasonCompleteStopAttack.BreakRequested -= StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested -= StartAttackCompletely;
            _attackBreaker.BreakRequested -= TryBreakRangedAttack;
        }

        public void Initialize(Player.Player player) => _player = player;

        private void ExtractComponents()
        {
            _enemyView = GetComponentInChildren<EnemyView>();
            _reasonCompleteStopAttack = GetComponent<ReasonCompleteStopAttack>();
            _attackBreaker = GetComponent<AttackBreaker>();
        }

        private void StartAttack()
        {
            StartCoroutine();
        }

        private void StopAttack()
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
                _enemyView.StopAttack();
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(_attackInterval);

                Vector2 direction = (_player.transform.position - transform.position).normalized;
                GameObject bulletGameObject = _spawnProjectile.ProjectileSpawnPoint(projectileEnemyPrefab.gameObject, direction, transform);
                ProjectileEnemy bullet = bulletGameObject.GetComponent<ProjectileEnemy>();
                bullet.Initialize(direction, bullet, _damage);
            }
        }

        private void StopAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.RangedAttack)
            {
                _isAttack = true;
                StopAttack();
            }
        }

        private void StartAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.RangedAttack)
            {
                StopCoroutine();

                _isAttack = false;

                if (_fovViewAttack.CheckPlayerInRadius())
                    StartCoroutine();
            }
        }

        private void TryBreakRangedAttack(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.RangedAttack)
            {
                StopAttack();
            }
            else if(type is BreakerEnemyType.RangedAttack)
            {
                StartAttack();
            }
        }
    }
}