using _Project.Enemy.Breakers;
using _Project.Enemy.Types;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy.Behaviors
{
    [RequireComponent(typeof(AgentMovement), typeof(MovementBreaker), typeof(ReasonCompleteStopMovement))]
    public class Chase : MonoBehaviour
    {
        private AgentMovement _agent;
        private FieldOfView _fov;
        private MovementBreaker _movementBreaker;
        private ReasonCompleteStopMovement _reasonCompleteStopMovement;

        private Coroutine _corutine;
        private bool _isMove;

        private void Awake()
        {
            ExtractComponents();

            _fov.OnSpotted += StartChasing;
            _fov.OnLost += StopChasing;
            _movementBreaker.BreakRequested += TryBreakChase;
            _reasonCompleteStopMovement.BreakRequested += StopMovementCompletely;
        }

        private void OnDestroy()
        {
            _fov.OnSpotted -= StartChasing;
            _fov.OnLost -= StopChasing;
            _movementBreaker.BreakRequested -= TryBreakChase;
            _reasonCompleteStopMovement.BreakRequested -= StopMovementCompletely;
        }

        private void ExtractComponents()
        {
            _agent = GetComponent<AgentMovement>();
            _fov = GetComponentInChildren<FieldOfView>();
            _movementBreaker = GetComponent<MovementBreaker>();
            _reasonCompleteStopMovement = GetComponent<ReasonCompleteStopMovement>();
        }

        private void StartChasing(Transform target)
        {
            if (_isMove == false)
            {
                _movementBreaker.Emit(MovementBreakReasonType.Chase);

                if (_corutine == null)
                    _corutine = StartCoroutine(ChaseRoutine(target));
            }
        }

        private void StopChasing(Transform target)
        {
            if (_isMove == false)
            {
                _movementBreaker.Emit(MovementBreakReasonType.Patrol);
                _movementBreaker.Emit(MovementBreakReasonType.MoveToTarget);
                StopCoroutine();
            }
        }

        private void StopCoroutine()
        {
            if (_corutine != null)
            {
                StopCoroutine(_corutine);
                _corutine = null;
            }
        }

        private IEnumerator ChaseRoutine(Transform target)
        {
            while (target != null)
            {
                _agent.Move(target.position);
                yield return null;
            }
        }

        private void TryBreakChase(MovementBreakReasonType reason)
        {
            if (reason is not MovementBreakReasonType.Chase)
            {
                StopCoroutine();
            }
        }

        private void StopMovementCompletely(MovementBreakReasonType type)
        {
            if (type is MovementBreakReasonType.Manual)
            {
                _isMove = true;
                StopCoroutine();
            }
            else if (type is MovementBreakReasonType.OnlyChase)
            {
                _isMove = true;
                StopCoroutine();
                _movementBreaker.Emit(MovementBreakReasonType.Manual);
                _movementBreaker.Emit(MovementBreakReasonType.Patrol);
                _movementBreaker.Emit(MovementBreakReasonType.MoveToTarget);
            }
            else if (type is MovementBreakReasonType.Chase)
            {
                _isMove = false;
            }
        }
    }
}