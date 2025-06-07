using Assets._Project.Scripts.Enemy;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy
{
    public class RangedAreaAttack : MonoBehaviour, IInitializePlayer
    {
        [SerializeField] private int _damage;
        [SerializeField] private FieldOfViewAttack _fovViewAttack;
        [SerializeField] private GameObject _primitivePrefab;
        [SerializeField] private float _attackCastTime;
        [SerializeField] private IncendiaryZoneEnemy _incendiaryZoneEnemyPrefab;
        [SerializeField] private float _radiusAttack;

        private Player.Player _player;

        private EnemyView _enemyView;
        private ReasonCompleteStopAttack _reasonCompleteStopAttack;
        private AttackBreaker _attackBreaker;

        private Coroutine _coroutine;
        private bool _isAttack;

        private void Awake()
        {
            ExtractComponents();
            _enemyView.Initialize();

            _fovViewAttack.OnPlayerAttack += StartAttack;
            _fovViewAttack.OnPlayerStopAttack += StopAttack;
            _reasonCompleteStopAttack.BreakRequested += StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested += StartAttackCompletely;
            _attackBreaker.BreakRequested += TryBreakMeleeAttack;
        }

        private void OnDestroy()
        {
            _fovViewAttack.OnPlayerAttack -= StartAttack;
            _fovViewAttack.OnPlayerStopAttack -= StopAttack;
            _reasonCompleteStopAttack.BreakRequested -= StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested -= StartAttackCompletely;
            _attackBreaker.BreakRequested -= TryBreakMeleeAttack;
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
                yield return new WaitForSeconds(_attackCastTime);

                if (_player.IsDie == false)
                {
                    IncendiaryZoneEnemy incendiaryZoneEnemy = Instantiate(_incendiaryZoneEnemyPrefab, _player.transform.position, Quaternion.identity, null);
                    incendiaryZoneEnemy.Initialize(_damage, _radiusAttack);
                }
            }
        }

        private void StopAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.RangedAreaAttack)
            {
                _isAttack = true;
                StopCorourine();
            }
        }

        private void StartAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.RangedAreaAttack)
            {
                StopCorourine();

                _isAttack = false;

                if (_fovViewAttack.CheckPlayerInRadius())
                    StartCoroutine();
            }
        }

        private void TryBreakMeleeAttack(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.RangedAreaAttack && type is not BreakerEnemyType.RangedAttack)
            {
                StopAttack();
            }
            else if (type is BreakerEnemyType.RangedAreaAttack)
            {
                StartAttack();
            }
        }
    }
}