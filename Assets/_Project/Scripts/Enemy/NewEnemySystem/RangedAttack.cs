using _Project.Weapon.Projectile;
using Assets._Project.Scripts.Enemy;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(ReasonCompleteStopAttack))]
    public class RangedAttack : MonoBehaviour, IInitializePlayer
    {
        [SerializeField] private ProjectileEnemy projectileEnemyPrefab;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackInterval;

        private Player.Player _player;

        private SpawnProjectile _spawnProjectile;

        private EnemyView _enemyView;
        private FieldOfViewAttack _fovViewAttack;
        private ReasonCompleteStopAttack _reasonCompleteStopAttack;

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
        }

        private void OnDestroy()
        {
            _fovViewAttack.OnPlayerAttack -= StartAttack;
            _fovViewAttack.OnPlayerStopAttack -= StopAttack;
            _reasonCompleteStopAttack.BreakRequested -= StopAttackCompletely;
        }

        public void Initialize(Player.Player player) => _player = player;

        private void ExtractComponents()
        {
            _enemyView = GetComponentInChildren<EnemyView>();
            _fovViewAttack = GetComponentInChildren<FieldOfViewAttack>();
            _reasonCompleteStopAttack = GetComponent<ReasonCompleteStopAttack>();
        }

        private void StartAttack()
        {
            if (_coroutine == null && _isAttack == false)
                _coroutine = StartCoroutine(Attack());
        }

        private void StopAttack()
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

        private void StopAttackCompletely(ReasonCompleteStopAttackType type)
        {
            if (type is not ReasonCompleteStopAttackType.RangedAttack)
            {
                _isAttack = true;
                StopAttack();
            }
            else if (type is ReasonCompleteStopAttackType.RangedAttack)
            {
                _isAttack = false;
                StartAttack();
            }
        }
    }
}