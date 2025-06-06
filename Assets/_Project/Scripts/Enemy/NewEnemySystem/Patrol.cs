using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(AgentMovement), typeof(MovementBreaker), typeof(ReasonCompleteStopMovement))]
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private int _randomPointIndex;

        private AgentMovement _agentMovement;
        private MovementBreaker _movementBreaker;
        private ReasonCompleteStopMovement _reasonCompleteStopMovement;

        private Vector3 _lastPosition;

        private void Awake()
        {
            ExtractComponents();

            _agentMovement.OnReachedDestination += MoveToRandomNextWaypoint;
            _movementBreaker.BreakRequested += TryBreakMovement;
            _reasonCompleteStopMovement.BreakRequested += StopMovementCompletely;
        }

        private void Start()
        {
            MoveToRandomNextWaypoint();
        }

        private void OnDestroy()
        {
            TryBreakMovement(MovementBreakReasonType.Manual);
            _movementBreaker.BreakRequested -= TryBreakMovement;
            _reasonCompleteStopMovement.BreakRequested -= StopMovementCompletely;
        }

        public void Initialize(Transform[] waypoints) => _waypoints = waypoints;

        private void ExtractComponents()
        {
            _agentMovement = GetComponent<AgentMovement>();
            _movementBreaker = GetComponent<MovementBreaker>();
            _reasonCompleteStopMovement = GetComponent<ReasonCompleteStopMovement>();
        }

        private void MoveToRandomNextWaypoint()
        {
            _randomPointIndex = Random.Range(0, _waypoints.Length);
            _lastPosition = _waypoints[_randomPointIndex].position;
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
            _agentMovement.Move(_lastPosition);
        }

        private void StopMovementCompletely(MovementBreakReasonType type)
        {
            if (type is MovementBreakReasonType.Manual)
            {
                _agentMovement.Agent.isStopped = true;
            }
            else if (type is MovementBreakReasonType.Patrol)
            {
                _agentMovement.Agent.isStopped = false;
                _agentMovement.Move(_lastPosition);
            }
        }
    }
}