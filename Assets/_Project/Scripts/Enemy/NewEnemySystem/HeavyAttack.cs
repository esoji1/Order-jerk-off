using _Project.ConstructionBuildings.Buildings;
using _Project.Core;
using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Enemy;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(ReasonCompleteStopAttack), typeof(AttackBreaker))]
    public class HeavyAttack : MonoBehaviour, IInitializePlayer
    {
        [SerializeField] private int _damage;
        [SerializeField] private FieldOfViewAttack _fovViewAttack;
        [SerializeField] private GameObject _primitivePrefab;
        [SerializeField] private float _attackCastTime;

        private Player.Player _player;

        private CreatingPrimitive _creatingPrimitive;

        private EnemyView _enemyView;
        private ReasonCompleteStopAttack _reasonCompleteStopAttack;
        private AttackBreaker _attackBreaker;
        private ReasonCompleteStopMovement _reasonCompleteStopMovement;

        private Coroutine _coroutine;
        private bool _isAttack;
        private Tween _tween;
        private bool _isDoesAttack;

        private void Awake()
        {
            ExtractComponents();
            _enemyView.Initialize();

            _creatingPrimitive = new CreatingPrimitive(_primitivePrefab);

            _fovViewAttack.OnPlayerAttack += StartAttack;
            _reasonCompleteStopAttack.BreakRequested += StopAttackCompletely;
            _attackBreaker.BreakRequested += TryBreakMeleeAttack;
        }

        private void Update()
        {
            if (_isDoesAttack == false)
            {
                StopAttack();
            }
        }

        private void OnDestroy()
        {
            if (_creatingPrimitive.CreatedPrimitive != null)
                GameObject.Destroy(_creatingPrimitive.CreatedPrimitive);

            _fovViewAttack.OnPlayerAttack -= StartAttack;
            _reasonCompleteStopAttack.BreakRequested -= StopAttackCompletely;
            _attackBreaker.BreakRequested -= TryBreakMeleeAttack;
        }

        public void Initialize(Player.Player player) => _player = player;

        private void ExtractComponents()
        {
            _enemyView = GetComponentInChildren<EnemyView>();
            _reasonCompleteStopAttack = GetComponent<ReasonCompleteStopAttack>();
            _attackBreaker = GetComponent<AttackBreaker>();
            _reasonCompleteStopMovement = GetComponent<ReasonCompleteStopMovement>();
        }

        private void StartAttack()
        {
            if (_coroutine == null && _isAttack == false)
                _coroutine = StartCoroutine(Attack());
        }

        private void StopAttack()
        {
            _reasonCompleteStopMovement.Emit(MovementBreakReasonType.Chase);
            StopCorourine();
        }

        private void StopCorourine()
        {
            if (_coroutine != null)
            {
                _isDoesAttack = true;
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
                _isDoesAttack = true;

                _enemyView.StartAttack();
                _creatingPrimitive.CreatePrimitive(_player.transform, 1);
                _tween = _creatingPrimitive.SpriteRenderer.DOFade(1, _attackCastTime);

                yield return new WaitForSeconds(_attackCastTime);

                if (CheckAttackHitRadius())
                    _player.Damage(_damage);

                //_currentNumberHits = 0;

                _enemyView.StopAttack();

                if (_creatingPrimitive.CreatedPrimitive != null)
                    GameObject.Destroy(_creatingPrimitive.CreatedPrimitive);

            }
            _isDoesAttack = false;
        }

        private bool CheckAttackHitRadius()
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(_creatingPrimitive.CreatedPrimitive.transform.position, 0.3f,
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

        private void TryBreakMeleeAttack(BreakerEnemyType type)
        {
            //if (type is not BreakerEnemyType.HeavyAttack)
            //{
            //    StopCorourine();
            //}
        }
    }
}