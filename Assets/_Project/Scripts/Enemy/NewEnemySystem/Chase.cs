using System.Collections;
using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(AgentMovement), typeof(MovementBreaker))]
    public class Chase : MonoBehaviour
    {
        private AgentMovement _agent;
        private FieldOfView _fov;
        private MovementBreaker _movementBreaker;

        private Coroutine _corutine;

        private void Awake()
        {
            ExtractComponents();

            _fov.OnPlayerSpotted += StartChasing;
            _fov.OnPlayerLost += StopChasing;
            _movementBreaker.BreakRequested += TryBreakChase;
        }

        private void OnDestroy()
        {
            _fov.OnPlayerSpotted -= StartChasing;
            _fov.OnPlayerLost -= StopChasing;
            _movementBreaker.BreakRequested += TryBreakChase;
        }

        private void ExtractComponents()
        {
            _agent = GetComponent<AgentMovement>();
            _fov = GetComponentInChildren<FieldOfView>();
            _movementBreaker = GetComponent<MovementBreaker>();
        }

        private void StartChasing(Player.Player target)
        {
            _movementBreaker.Emit(MovementBreakReasonType.Chase);

            if (_corutine == null)
                _corutine = StartCoroutine(ChaseRoutine(target));
        }

        private void StopChasing(Player.Player target)
        {
            _movementBreaker.Emit(MovementBreakReasonType.Patrol);

            if (_corutine != null)
            {
                StopCoroutine(_corutine);
                _corutine = null;
            }
        }

        private IEnumerator ChaseRoutine(Player.Player target)
        {
            while (target != null)
            {
                _agent.Move(target.transform.position);
                yield return null;
            }
        }

        private void TryBreakChase(MovementBreakReasonType reason)
        {
            if (reason is not MovementBreakReasonType.Chase)
            {
                StopCoroutine(_corutine);
                _corutine = null;
            }
        }
    }
}