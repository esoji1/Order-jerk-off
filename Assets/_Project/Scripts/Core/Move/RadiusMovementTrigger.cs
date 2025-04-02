using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Weapon;
using UnityEngine;

namespace Assets._Project.Sctipts.Core
{
    public class RadiusMovementTrigger : MonoBehaviour
    {
        private Transform _pointDetection;
        private Transform _radiusSprite;
        private LayerMask _layer;
        private Transform _currentTransfomMove;
        private float _speed;
        private float _visibilityRadius;

        private DetectionRadius _detectionRadius;
        private Move _move;

        private bool _isMove;

        public void Initialize(Transform pointDetection, Transform radiusSprite, LayerMask layer, Transform currentTransfomMove, float speed, float radius)
        {
            _pointDetection = pointDetection;
            _radiusSprite = radiusSprite;
            _layer = layer;
            _currentTransfomMove = currentTransfomMove;
            _speed = speed;
            _visibilityRadius = radius;

            InitializeInside();
        }

        public void MoveToTarget(float attackRadius)
        {
            _detectionRadius.Detection(_visibilityRadius);

            if (_detectionRadius.HasEnemies && _isMove)
            {
                Vector2 _direction = (_detectionRadius.GetNearestEnemy().transform.position - _pointDetection.position).normalized;
                float distance = Vector2.Distance(_currentTransfomMove.position, _detectionRadius.GetNearestEnemy().transform.position);

                if (distance > attackRadius)
                {
                    _move.Rotation(_radiusSprite, _direction);
                    _move.MoveTarget(_detectionRadius.GetNearestEnemy().transform, _currentTransfomMove, _speed);
                }
            }
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