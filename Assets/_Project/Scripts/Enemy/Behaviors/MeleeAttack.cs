using _Project.Core.Interface;
using _Project.Enemy.Breakers;
using _Project.Enemy.Types;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy.Behaviors
{
    [RequireComponent(typeof(ReasonCompleteStopAttack), typeof(AttackBreaker))]
    public class MeleeAttack : MonoBehaviour, IInitializePlayer, IInitializeTarget
    {
        [SerializeField] private int _damage;
        [SerializeField] private FieldOfViewAttack _fovViewAttack;

        private Player.Player _player;
        private Transform _target;

        private EnemyView _enemyView;
        private ReasonCompleteStopAttack _reasonCompleteStopAttack;
        private AttackBreaker _attackBreaker;

        private Coroutine _coroutine;
        private bool _isAttack;

        private void Awake()
        {
            ExtractComponents();
            _enemyView.Initialize();

            _fovViewAttack.OnAttack += StartAttack;
            _fovViewAttack.OnStopAttack += StopAttack;
            _reasonCompleteStopAttack.BreakRequested += StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested += StartAttackCompletely;
            _attackBreaker.BreakRequested += TryBreakMeleeAttack;
        }

        private void OnDestroy()
        {
            _fovViewAttack.OnAttack -= StartAttack;
            _fovViewAttack.OnStopAttack -= StopAttack;
            _reasonCompleteStopAttack.BreakRequested -= StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested -= StartAttackCompletely;
            _attackBreaker.BreakRequested -= TryBreakMeleeAttack;
        }

        public void Initialize(Player.Player player) => _player = player;

        public void Initialize(Transform target) => _target = target;

        private void ExtractComponents()
        {
            _enemyView = GetComponentInChildren<EnemyView>();
            _reasonCompleteStopAttack = GetComponent<ReasonCompleteStopAttack>();
            _attackBreaker = GetComponent<AttackBreaker>();
        }

        private void StartAttack()
        {
            _attackBreaker.Emit(BreakerEnemyType.MeleeAttack);
            StartCoroutine();
        }

        private void StopAttack()
        {
            _attackBreaker.Emit(BreakerEnemyType.RangedAttack);
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
                _enemyView.StopAttack();
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                _enemyView.StartAttack();
                float attackAnimationTime = _enemyView.Animator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(attackAnimationTime);

                if (_fovViewAttack.CheckPlayerInRadius())
                {
                    _player.Damage(_damage);
                }
                else if (_fovViewAttack.CheckBaseBuildingInRadius())
                {
                    _fovViewAttack.ReturnCurrenBuildTarget().Damage(_damage);
                }
            }
        }

        private void StopAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.MeleeAttack)
            {
                _isAttack = true;
                StopCorourine();
            }
        }

        private void StartAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.MeleeAttack)
            {
                StopCorourine();

                _isAttack = false;

                if (_fovViewAttack.CheckPlayerInRadius())
                    StartCoroutine();
            }
        }

        private void TryBreakMeleeAttack(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.MeleeAttack)
            {
                StopCorourine();
            }
        }
    }
}
