using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _stoppingDistance;

        private NavMeshAgent _agent;

        public event Action OnReachedDestination;

        public NavMeshAgent Agent => _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _agent.speed = _moveSpeed;
            _agent.stoppingDistance = _stoppingDistance;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _agent.acceleration = 30f;
        }

        public void Move(Vector3 toDestination)
        {
            _agent.SetDestination(toDestination);
            StartCoroutine(ReachDestination());
        }

        private IEnumerator ReachDestination()
        {
            yield return new WaitUntil(() =>
            {
                float distance = Vector2.Distance(_agent.destination, transform.position);

                return distance <= _agent.stoppingDistance;
            });

            OnReachedDestination?.Invoke();
        }
    }
}