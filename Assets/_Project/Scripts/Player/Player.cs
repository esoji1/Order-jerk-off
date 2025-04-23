using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Core.HealthSystem;
using Assets._Project.Scripts.Core.Interface;
using Assets._Project.Scripts.ExperienceBar;
using Assets._Project.Scripts.Player.Movement;
using Assets._Project.Scripts.Player.Pumping;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.SelectionGags;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Core.HealthSystem;
using Assets._Project.Sctipts.JoystickMovement;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    public class Player : MonoBehaviour, IGagsPicker, IOnDamage, IDamage
    {
        private PlayerConfig _config;
        private JoysickForMovement _joysickForMovement;
        private PointRotation _rotationSprite;
        private WallDetection[] _wallDetection;
        private PlayerCharacteristics _playerCharacteristics;
        private LevelPlayer _levelPlayer;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _dynamic;
        private UseWeapons.UseWeapons _useWeapons;

        private PlayerMovement _playerMovement;
        private Flip _flip;
        private Health _health;
        private Wallet.Wallet _wallet;

        private PlayerView _playerView;
        private PointHealth _pointHealth;
        private CircleRadiusVisualizer _circleRadiusVisualizer;
        private RadiusMovementTrigger _radiusMovementTrigger;

        private Weapon.Weapons.Weapon _weapon;
        private HealthView _healthView;
        private HealthInfo _healthInfo;

        private bool _isMove;

        public event Action<int> OnDamage;
        public event Action OnUp;
        public event Action OnAddExperience;

        public float Speed => _config.Speed;
        public JoysickForMovement JoysickForMovement => _joysickForMovement;
        public WallDetection[] WallDetection => _wallDetection;
        public PlayerConfig Config => _config;
        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerCharacteristics PlayerCharacteristics => _playerCharacteristics;
        public PointHealth PointHealth => _pointHealth;
        public PointRotation PointRotation => _rotationSprite;
        public Wallet.Wallet Wallet => _wallet;
        public HealthView HealthView => _healthView;
        public Health Health => _health;

        private void Update()
        {
            if (_isMove)
                Move();
        }

        private void OnDestroy()
        {
            _levelPlayer.OnLevelUp -= ImproveCharacteristics;
            _health.OnDie += Die;
            _useWeapons.OnChangeWeapon -= AppropriateWeapons;
        }

        public void Initialize(PlayerConfig config, JoysickForMovement joysickForMovement, LevelPlayer levelPlayer, HealthInfo healthInfoPrefab,
            HealthView healthViewPrefab, Canvas dynamic, UseWeapons.UseWeapons useWeapons)
        {
            _config = config;
            _joysickForMovement = joysickForMovement;
            _levelPlayer = levelPlayer;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _dynamic = dynamic;
            _useWeapons = useWeapons;
            _isMove = true;

            InitializeInside();

            _levelPlayer.OnLevelUp += ImproveCharacteristics;
            _health.OnDie += Die;
            _useWeapons.OnChangeWeapon += AppropriateWeapons;

        }

        public void AddExperience(int value)
        {
            _levelPlayer.AddExperience(value);
            OnAddExperience?.Invoke();
        }

        public void Damage(int damage)
        {
            _health.TakeDamage(damage);
            OnDamage?.Invoke(damage);
        }

        public void AddMoney(int value)
        {
            _wallet.AddMoney(value);
        }

        public void SetMove(bool value)
        {
            _isMove = value;
        }

        private void InitializeInside()
        {
            ExtractComponents();
            InitializeCharacteristics();

            _playerMovement = new PlayerMovement(_joysickForMovement, this);
            _flip = new Flip();
            _wallet = new Wallet.Wallet(0);
            _playerView.Initialize();

            _healthInfo = _healthInfoPrefab;
            _healthInfo.Initialize(_dynamic);

            _healthView = _healthViewPrefab;
            _healthView.Initialize(this, _playerCharacteristics.Health, _healthInfo, this);

            _circleRadiusVisualizer.Initialize(transform);
            _radiusMovementTrigger.Initialize(transform, _config.LayerEnemy, transform, _config.Speed);
        }

        private void ExtractComponents()
        {
            _playerView = GetComponentInChildren<PlayerView>();
            _wallDetection = GetComponentsInChildren<WallDetection>();
            _pointHealth = GetComponentInChildren<PointHealth>();
            _circleRadiusVisualizer = GetComponentInChildren<CircleRadiusVisualizer>();
            _radiusMovementTrigger = GetComponent<RadiusMovementTrigger>();
            _rotationSprite = GetComponentInChildren<PointRotation>();
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
            _health.SetHealth(_playerCharacteristics.Health);
            _playerCharacteristics.AttackSpeed += 1;
            _playerCharacteristics.ReturnInitialAttackPosition += 0.02f;
            _playerCharacteristics.AddDamageAttack += 2;
            OnUp?.Invoke();

            _weapon.WeaponData.ExtraDamage = _playerCharacteristics.AddDamageAttack;
            _weapon.WeaponData.ReturnInitialAttackPosition = _playerCharacteristics.ReturnInitialAttackPosition;
        }

        private void Move()
        {
            _playerMovement.Move();

            if (_weapon != null)
                _radiusMovementTrigger.MoveToTarget(_weapon.Config.RadiusAttack, _weapon.Config.VisibilityRadius);

            MoveInRadiusAndRotation();
            _flip.RotateView(_joysickForMovement.VectorDirection(), _rotationSprite.transform);

            if (_joysickForMovement.VectorDirection().y > 0f)
            {
                _playerView.SpriteRenderer.sprite = _playerView.Back;
                _weapon.SpriteRenderer.sortingLayerName = "AddInPlayer";
                _weapon.SpriteRenderer.sortingOrder = 0;
            }
            else if (_joysickForMovement.VectorDirection().y < 0f)
            {
                _playerView.SpriteRenderer.sprite = _playerView.Front;
                _weapon.SpriteRenderer.sortingLayerName = "Weapon";
                _weapon.SpriteRenderer.sortingOrder = 10;
            }

            _circleRadiusVisualizer.DrawRadius(_weapon.Config.VisibilityRadius);
        }

        private void MoveInRadiusAndRotation()
        {
            if (_joysickForMovement.VectorDirection() != Vector2.zero)
            {
                _radiusMovementTrigger.StopRadiusMovement();
            }
            else if (_joysickForMovement.VectorDirection() == Vector2.zero)
            {
                _radiusMovementTrigger.StartRadiusMovement();
            }
        }

        private void AppropriateWeapons(Weapon.Weapons.Weapon weapon)
        {
            _weapon = weapon;
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
