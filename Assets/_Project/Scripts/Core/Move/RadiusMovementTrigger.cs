using _Project.Weapon;
using UnityEngine;

namespace _Project.Core
{
    public class RadiusMovementTrigger : MonoBehaviour
    {
        private Transform _pointDetection;
        private LayerMask _layer;
        private Transform _currentTransfomMove;
        private float _speed;

        private DetectionRadius _detectionRadius;
        private Move _move;

        private bool _isMove;

        public void Initialize(Transform pointDetection, LayerMask layer, Transform currentTransfomMove, float speed)
        {
            _pointDetection = pointDetection;
            _layer = layer;
            _currentTransfomMove = currentTransfomMove;
            _speed = speed;

            InitializeInside();
        }

        public bool MoveToTarget(float attackRadius, float visibilityRadius)
        {
            _detectionRadius.Detection(visibilityRadius);

            if (_detectionRadius.HasEnemies && _isMove)
            {
                Vector2 _direction = (_detectionRadius.GetNearestEnemy().transform.position - _pointDetection.position).normalized;
                float distance = Vector2.Distance(_currentTransfomMove.position, _detectionRadius.GetNearestEnemy().transform.position);

                if (distance > attackRadius)
                {
                    _move.MoveTarget(_detectionRadius.GetNearestEnemy().transform, _currentTransfomMove, _speed);
                    return true;
                }
                else if (distance < visibilityRadius)
                {
                    return true;
                }
            }
            return false;
        }

        public void StartRadiusMovement() => _isMove = true;
        public void StopRadiusMovement() => _isMove = false;

        private void InitializeInside()
        {
            _isMove = true;
            _detectionRadius = new DetectionRadius(_pointDetection, _layer);
            _move = new Move();
        }
    }
}