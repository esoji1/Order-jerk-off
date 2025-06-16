using _Project.Core.Interface;
using _Project.Enemy.Breakers;
using _Project.Enemy.Types;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy.Behaviors
{
    [RequireComponent(typeof(AgentMovement), typeof(MovementBreaker), typeof(ReasonCompleteStopMovement))]
    public class MoveToTarget : MonoBehaviour, IInitializeTarget
    {
        private Transform _target;

        private AgentMovement _agent;
        private MovementBreaker _movementBreaker;
        private ReasonCompleteStopMovement _reasonCompleteStopMovement;

        private Coroutine _corutine;
        private bool _isMove;

        private void Awake()
        {
            ExtractComponents();

            _agent.OnReachedDestination += Move;
            _movementBreaker.BreakRequested += TryBreakChase;
            _reasonCompleteStopMovement.BreakRequested += StopMovementCompletely;
        }

        private void Start()
        {
            Move();
        }

        private void OnDestroy()
        {
            _agent.OnReachedDestination -= Move;
            _movementBreaker.BreakRequested -= TryBreakChase;
            _reasonCompleteStopMovement.BreakRequested -= StopMovementCompletely;
        }

        public void Initialize(Transform point) => _target = point;

        private void ExtractComponents()
        {
            _agent = GetComponent<AgentMovement>();
            _movementBreaker = GetComponent<MovementBreaker>();
            _reasonCompleteStopMovement = GetComponent<ReasonCompleteStopMovement>();
        }

        private void Move()
        {
            _agent.Move(_target.position);
        }

        private void StartMoveToTarget()
        {
            if (_isMove == false)
            {
                if (_corutine == null)
                    _corutine = StartCoroutine(MoveToTargetRoutine());
            }
        }

        private void StopMoveToTarget()
        {
            if (_isMove == false)
            {
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

        private IEnumerator MoveToTargetRoutine()
        {
            while (_target != null)
            {
                _agent.Move(_target.position);
                yield return null;
            }
        }

        private void TryBreakChase(MovementBreakReasonType reason)
        {
            if (reason is not MovementBreakReasonType.MoveToTarget)
            {
                _agent.OnReachedDestination -= Move;
            }
            else if(reason is MovementBreakReasonType.MoveToTarget)
            {
                _agent.OnReachedDestination += Move;
            }
        }

        private void StopMovementCompletely(MovementBreakReasonType type)
        {
            if (type is MovementBreakReasonType.Manual)
            {
                _agent.Agent.isStopped = true;
            }
            else if (type is MovementBreakReasonType.MoveToTarget)
            {
                _agent.Agent.isStopped = false;
                _agent.Move(_target.position);
            }
        }
    }
}
