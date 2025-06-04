using UnityEngine;
using UnityEngine.AI;

namespace _Project.Enemy.MovePoints
{
    public class MoveToPoint : MonoBehaviour, IMovePoints
    {
        private Transform _mainBueldPoint;
        private NavMeshAgent _agent;

        public void Initialize(Transform mainBueldPoint, NavMeshAgent agent)
        {
            _mainBueldPoint = mainBueldPoint;
            _agent = agent;
        }

        public void MovePoints()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_mainBueldPoint.position);
        }
    }
}