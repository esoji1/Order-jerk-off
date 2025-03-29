using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Player.Movement;
using Assets._Project.Scripts.Player.Pumping;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.JoystickMovement;
using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private JoysickForMovement _joysickForMovement;
        [SerializeField] private Transform _rotation;
        [SerializeField] private WallDetection[] _wallDetection;
        [SerializeField] private PlayerCharacteristics _playerCharacteristics;
        [SerializeField] private LevelPlayer _levelPlayer;
        [SerializeField] private CharacteristicsView _characteristicsView;

        private PlayerMovement _playerMovement;
        private Flip _flip;
        private Health _health;

        private PlayerView _playerView;
       
        public int Speed => _config.Speed;
        public JoysickForMovement JoysickForMovement => _joysickForMovement;
        public WallDetection[] WallDetection => _wallDetection;
        public PlayerConfig Config => _config;
        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerCharacteristics PlayerCharacteristics => _playerCharacteristics;

        private void Awake()
        {
            ExtractComponents();
            InitializeCharacteristics();

            _playerMovement = new PlayerMovement(_joysickForMovement, this);
            _flip = new Flip();
            _playerView.Initialize();
            _characteristicsView.Initialize(this);
        }

        private void Update()
        {
            _playerMovement.Move();

            _flip.RotateDirections(_joysickForMovement.VectorDirection(), _rotation);
            _flip.RotateView(_joysickForMovement.VectorDirection(), _rotation);
        }

        private void OnEnable()
        {
            _levelPlayer.OnLevelUp += ImproveCharacteristics;
        }

        private void OnDisable()
        {
            _levelPlayer.OnLevelUp -= ImproveCharacteristics;
        }

        private void ExtractComponents()
        {
            _playerView = GetComponentInChildren<PlayerView>();
            _wallDetection = GetComponentsInChildren<WallDetection>();
        }

        private void InitializeCharacteristics()
        {
            _health = new Health(_config.Health);
            _playerCharacteristics = new PlayerCharacteristics();
            _playerCharacteristics.Health = _health.HealthValue;
        }

        private void ImproveCharacteristics()
        {
            _playerCharacteristics.Health += 20;
            _health.AddHealth(_playerCharacteristics.Health);
            _playerCharacteristics.AttackSpeed += 1;
            _playerCharacteristics.ReturnInitialAttackPosition += 0.02f;
            _playerCharacteristics.AddDamageAttack += 2;

            _characteristicsView.Show();    
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _config.AttackRadius);
        }
    }
}