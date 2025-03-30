using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Core.HealthSystem;
using Assets._Project.Scripts.Core.Interface;
using Assets._Project.Scripts.Player.Movement;
using Assets._Project.Scripts.Player.Pumping;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.SelectionGags;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Core.HealthSystem;
using Assets._Project.Sctipts.JoystickMovement;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    public class Player : MonoBehaviour, IGagsPicker, IOnDamage, IDamage
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private JoysickForMovement _joysickForMovement;
        [SerializeField] private Transform _rotation;
        [SerializeField] private WallDetection[] _wallDetection;
        [SerializeField] private PlayerCharacteristics _playerCharacteristics;
        [SerializeField] private LevelPlayer _levelPlayer;
        [SerializeField] private CharacteristicsView _characteristicsView;
        [SerializeField] private HealthInfo _healthInfoPrefab;
        [SerializeField] private HealthView _healthViewPrefab;
        [SerializeField] private Canvas _dynamic;

        private PlayerMovement _playerMovement;
        private Flip _flip;
        private Health _health;
        private HealthView _healthView;
        private HealthInfo _healthInfo;

        private PlayerView _playerView;
        private PointHealth _pointHealth;

        public event Action<int> OnDamage;
        public event Action OnUp;

        public int Speed => _config.Speed;
        public JoysickForMovement JoysickForMovement => _joysickForMovement;
        public WallDetection[] WallDetection => _wallDetection;
        public PlayerConfig Config => _config;
        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerCharacteristics PlayerCharacteristics => _playerCharacteristics;

        public PointHealth PointHealth => _pointHealth;

        private void Awake()
        {
            ExtractComponents();
            InitializeCharacteristics();

            _playerMovement = new PlayerMovement(_joysickForMovement, this);
            _flip = new Flip();
            _playerView.Initialize();
            _characteristicsView.Initialize(this);

            _healthInfo = Instantiate(_healthInfoPrefab, transform.position, Quaternion.identity);
            _healthInfo.Initialize(_dynamic);
            _healthView = Instantiate(_healthViewPrefab, transform.position, Quaternion.identity);
            _healthView.Initialize(this, _playerCharacteristics.Health, _healthInfo, this);

            StartCoroutine(GradualHealing());

        }

        private void Update()
        {
            _playerMovement.Move();

            _flip.RotateDirections(_joysickForMovement.VectorDirection(), _rotation);
            _flip.RotateView(_joysickForMovement.VectorDirection(), _rotation);
            _healthView.FollowTargetHealth();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Damage(20);
            }
        }

        private void OnEnable()
        {
            _levelPlayer.OnLevelUp += ImproveCharacteristics;
            _health.OnDie += Die;
        }

        private void OnDisable()
        {
            _levelPlayer.OnLevelUp -= ImproveCharacteristics;
            _health.OnDie += Die;
        }

        public void AddExperience(int value)
        {
            _levelPlayer.AddExperience(value);
        }

        public void Damage(int damage)
        {
            _health.TakeDamage(damage);
            OnDamage?.Invoke(damage);
        }

        private void ExtractComponents()
        {
            _playerView = GetComponentInChildren<PlayerView>();
            _wallDetection = GetComponentsInChildren<WallDetection>();
            _pointHealth = GetComponentInChildren<PointHealth>();
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
            OnUp?.Invoke();

            _characteristicsView.Show();
        }

        private IEnumerator GradualHealing()
        {
            while (true)
            {
                _health.AddHealth(10);
                _healthView.AddHealth(10);
                yield return new WaitForSeconds(5f);
            }
        }

        private void Die()
        {
            Destroy(_healthInfo.InstantiatedHealthBar.gameObject);
            Destroy(_healthInfo.gameObject);
            Destroy(_healthView.gameObject);
            Destroy(gameObject); 
        }
    }
}
