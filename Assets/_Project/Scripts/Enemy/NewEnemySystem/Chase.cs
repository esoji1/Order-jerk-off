using _Project.Player;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AgentMovement))]
public class Chase : MonoBehaviour
{
    [SerializeField] private FieldOfView _fov;
    [SerializeField] private MovementBreaker _movementBreaker;

    private AgentMovement _agent;
    private Coroutine _corutine;

    private void Awake()
    {
        _agent = GetComponent<AgentMovement>();

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

    private void StartChasing(Player target)
    {
        _movementBreaker.Emit(MovementBreakReasonType.Chase);

        if (_corutine == null)
            _corutine = StartCoroutine(ChaseRoutine(target));
    }

    private void StopChasing(Player target)
    {
        _movementBreaker.Emit(MovementBreakReasonType.Patrol);

        if (_corutine != null)
        {
            StopCoroutine(_corutine);
            _corutine = null;
        }
    }

    private IEnumerator ChaseRoutine(Player target)
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
