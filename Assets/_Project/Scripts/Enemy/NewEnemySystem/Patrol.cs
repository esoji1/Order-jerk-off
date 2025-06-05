using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(AgentMovement))]
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private int _randomPointIndex;

        private AgentMovement _agentMovement;
        private MovementBreaker _movementBreaker;

        private void Awake()
        {
            ExtractComponents();

            _agentMovement.OnReachedDestination += MoveToRandomNextWaypoint;
            _movementBreaker.BreakRequested += TryBreakMovement;
        }

        private void Start()
        {
            MoveToRandomNextWaypoint();
        }

        private void OnDestroy()
        {
            TryBreakMovement(MovementBreakReasonType.Manual);
            _movementBreaker.BreakRequested -= TryBreakMovement;
        }

        public void Initialize(Transform[] waypoints) => _waypoints = waypoints;

        private void ExtractComponents()
        {
            _agentMovement = GetComponent<AgentMovement>();
            _movementBreaker = GetComponent<MovementBreaker>();
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
}