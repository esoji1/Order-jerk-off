using _Project.ConstructionBuildings.Buildings;
using _Project.Core;
using _Project.Core.Interface;
using _Project.Enemy.Breakers;
using _Project.Enemy.Types;
using Assets._Project.Scripts.Core;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy.Behaviors
{
    [RequireComponent(typeof(ReasonCompleteStopAttack), typeof(AttackBreaker))]
    public class HeavyAttack : MonoBehaviour, IInitializePlayer, IInitializeTarget
    {
        [SerializeField] private int _damage;
        [SerializeField] private FieldOfViewAttack _fovViewAttack;
        [SerializeField] private GameObject _primitivePrefab;
        [SerializeField] private float _attackCastTime;
        [SerializeField] private float _radiusAttack;

        private Player.Player _player;
        private Transform _target;

        private CreatingPrimitive _creatingPrimitive;

        private EnemyView _enemyView;
        private ReasonCompleteStopAttack _reasonCompleteStopAttack;
        private AttackBreaker _attackBreaker;
        private ReasonCompleteStopMovement _reasonCompleteStopMovement;

        private Coroutine _coroutine;
        private bool _isAttack;
        private Tween _tween;

        private void Awake()
        {
            ExtractComponents();
            _enemyView.Initialize();

            _creatingPrimitive = new CreatingPrimitive(_primitivePrefab);

            _fovViewAttack.OnAttack += StartAttack;
            _reasonCompleteStopAttack.BreakRequested += StopAttackCompletely;
            _reasonCompleteStopAttack.StartingRequested += StartAttackCompletely;
            _attackBreaker.BreakRequested += TryBreakMeleeAttack;
        }

        private void OnDestroy()
        {
            _tween.Kill();
            if (_creatingPrimitive.CreatedPrimitive != null)
                GameObject.Destroy(_creatingPrimitive.CreatedPrimitive);

            _fovViewAttack.OnAttack -= StartAttack;
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
            _reasonCompleteStopMovement = GetComponent<ReasonCompleteStopMovement>();
        }

        private void StartAttack()
        {
            _attackBreaker.Emit(BreakerEnemyType.HeavyAttack);
            StartCoroutine();
        }

        private void StopAttack()
        {
            _reasonCompleteStopMovement.Emit(MovementBreakReasonType.Chase);
            _reasonCompleteStopMovement.Emit(MovementBreakReasonType.Patrol);
            _attackBreaker.Emit(BreakerEnemyType.RangedAreaAttack);
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
            if (_creatingPrimitive.CreatedPrimitive != null)
                GameObject.Destroy(_creatingPrimitive.CreatedPrimitive);

            if (_coroutine != null)
            {
                _tween.Kill();
                _enemyView.StopAttack();
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator Attack()
        {
            while (_fovViewAttack.CheckPlayerInRadius())
            {
                _reasonCompleteStopMovement.Emit(MovementBreakReasonType.Manual);

                _enemyView.StartAttack();
                _creatingPrimitive.CreatePrimitive(_player.transform, 1);
                _tween = _creatingPrimitive.SpriteRenderer.DOFade(1, _attackCastTime);

                yield return _tween.WaitForCompletion();

                if (CheckAttackHitRadius())
                {
                    if (_fovViewAttack.CheckPlayerInRadius())
                    {
                        _player.Damage(_damage);
                    }
                    else if (_fovViewAttack.CheckBaseBuildingInRadius())
                    {
                        _fovViewAttack.ReturnCurrenBuildTarget().Damage(_damage);
                    }
                }

                _enemyView.StopAttack();

                if (_creatingPrimitive.CreatedPrimitive != null)
                    GameObject.Destroy(_creatingPrimitive.CreatedPrimitive);

            }
            StopAttack();
        }

        private bool CheckAttackHitRadius()
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(_creatingPrimitive.CreatedPrimitive.transform.position, _radiusAttack,
                Layers.LayerPlayer);

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out Player.Player _) || collider.TryGetComponent(out BaseBuilding _))
                {
                    return true;
                }
            }

            return false;
        }

        private void StopAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.HeavyAttack)
            {
                _isAttack = true;
                StopCorourine();
            }
        }

        private void StartAttackCompletely(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.HeavyAttack)
            {
                StopCorourine();

                _isAttack = false;

                if (_fovViewAttack.CheckPlayerInRadius())
                    StartCoroutine();
            }
        }

        private void TryBreakMeleeAttack(BreakerEnemyType type)
        {
            if (type is not BreakerEnemyType.HeavyAttack)
            {
                StopCorourine();
            }
        }
    }
}