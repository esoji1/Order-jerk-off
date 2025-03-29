using UnityEngine;

namespace Assets._Project.Scripts.Player.Movement
{
    public class WallDetection : MonoBehaviour
    {
        [SerializeField] private float _raycastDistance = 1f;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private Player _player;

        private bool _isTouchingWall;
        private RaycastHit2D _lastHit;

        public bool IsTouchingWall => _isTouchingWall;

        private void Update()
        {
            _lastHit = Physics2D.Raycast(transform.position, _player.JoysickForMovement.VectorDirection(), _raycastDistance, _wallLayer);
            bool hitWall = _lastHit.collider != null;

            _isTouchingWall = hitWall;
                Debug.DrawRay(transform.position, _player.JoysickForMovement.VectorDirection() * _raycastDistance,
                    hitWall ? Color.green : Color.red);
        }
    }
}