using UnityEngine;

[RequireComponent(typeof(AgentMovement))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private int _randomPointIndex;
    [SerializeField] private MovementBreaker _movementBreaker;

    private AgentMovement _agentMovement;

    private void Awake()
    {
        _agentMovement = GetComponent<AgentMovement>();
    }

    private void Start()
    {
        MoveToRandomNextWaypoint();

        _agentMovement.OnReachedDestination += MoveToRandomNextWaypoint;
        _movementBreaker.BreakRequested += TryBreakMovement;
    }

    private void OnDestroy()
    {
        TryBreakMovement(MovementBreakReasonType.Manual);
        _movementBreaker.BreakRequested -= TryBreakMovement;
    }

    private void MoveToRandomNextWaypoint()
    {
        _randomPointIndex = Random.Range(0, _waypoints.Length);
        Move();
    }

    private void TryBreakMovement(MovementBreakReasonType reason)
    {
        if (reason is not MovementBreakReasonType.Patrol)
            _agentMovement.OnReachedDestination -= MoveToRandomNextWaypoint;
        else if (reason is MovementBreakReasonType.Patrol)
            _agentMovement.OnReachedDestination += MoveToRandomNextWaypoint;
    }

    private void Move()
    {
        _agentMovement.Move(_waypoints[_randomPointIndex].position);
    }
}