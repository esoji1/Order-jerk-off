using Assets._Project.Scripts.Player.Movement;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.JoystickMovement;
using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _speed = 5;
        [SerializeField] private JoysickForMovement _joysickForMovement;
        [SerializeField] private Transform _rotation;
        [SerializeField] private WallDetection _wallDetection;

        private PlayerMovement _playerMovement;
        private Flip _flip;

        private PlayerView _playerView;

        public int Speed => _speed;
        public JoysickForMovement JoysickForMovement => _joysickForMovement;
        public WallDetection WallDetection => _wallDetection;

        private void Awake()
        {
            ExtractComponents();

            _playerMovement = new PlayerMovement(_joysickForMovement, this);
            _flip = new Flip();
            _playerView.Initialize();
        }

        private void Update()
        {
            _playerMovement.Move();
            _flip.RotateDirections(_joysickForMovement.VectorDirection(), _rotation);
            _flip.RotateView(_joysickForMovement.VectorDirection(), _rotation);
        }

        private void ExtractComponents()
        {
            _playerView = GetComponentInChildren<PlayerView>();
        }
    }
}