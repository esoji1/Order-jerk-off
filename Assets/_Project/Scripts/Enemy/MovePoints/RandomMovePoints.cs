using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets._Project.Scripts.Enemy.MovePoints
{
    public class RandomMovePoints : IMovePoints
    {
        private List<Transform> _points;
        private NavMeshAgent _agent;

        public RandomMovePoints(List<Transform> points, NavMeshAgent agent)
        {
            _points = new List<Transform>(points);
            _agent = agent;
        }

        public void MovePoints()
        {
            Transform point = _points[Random.Range(0, _points.Count)];
            _agent.isStopped = false;
            if (_agent.pathPending == false && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (_agent.hasPath == false || _agent.velocity.sqrMagnitude == 0f)
                {
                    _agent.SetDestination(point.position);
                }
            }
        }
    }
}