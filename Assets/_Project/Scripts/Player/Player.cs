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
        private PlayerConfig _config;
        private JoysickForMovement _joysickForMovement;
        private PointRotation _rotationSprite;
        private WallDetection[] _wallDetection;
        private PlayerCharacteristics _playerCharacteristics;
        private LevelPlayer _levelPlayer;
        private CharacteristicsView _characteristicsView;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _dynamic;
        private UseWeapons.UseWeapons _useWeapons;

        private PlayerMovement _playerMovement;
        private Flip _flip;
        private Move _move;
        private Health _health;
        private HealthView _healthView;
        private HealthInfo _healthInfo;

        private PlayerView _playerView;
        private PointHealth _pointHealth;
        private CircleRadiusVisualizer _circleRadiusVisualizer;
        private RadiusMovementTrigger _radiusMovementTrigger;

        private Weapon.Weapons.Weapon _weapon;

        public event Action<int> OnDamage;
        public event Action OnUp;

        public float Speed => _config.Speed;
        public JoysickForMovement JoysickForMovement => _joysickForMovement;
        public WallDetection[] WallDetection => _wallDetection;
        public PlayerConfig Config => _config;
        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerCharacteristics PlayerCharacteristics => _playerCharacteristics;
        public PointHealth PointHealth => _pointHealth;
        public PointRotation PointRotation => _rotationSprite;

        private void Update()
        {
            Move();
        }

        private void OnDestroy()
        {
            _levelPlayer.OnLevelUp -= ImproveCharacteristics;
            _health.OnDie += Die;
            _useWeapons.OnChangeWeapon -= AppropriateWeapons;
        }

        public void Initialize(PlayerConfig config, JoysickForMovement joysickForMovement, LevelPlayer levelPlayer, CharacteristicsView characteristicsView,
            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas dynamic, UseWeapons.UseWeapons useWeapons)
        {
            _config = config;
            _joysickForMovement = joysickForMovement;
            _levelPlayer = levelPlayer;
            _characteristicsView = characteristicsView;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _dynamic = dynamic;
            _useWeapons = useWeapons;

            InitializeInside();

            _levelPlayer.OnLevelUp += ImproveCharacteristics;
            _health.OnDie += Die;
            _useWeapons.OnChangeWeapon += AppropriateWeapons;

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

        private void InitializeInside()
        {
            ExtractComponents();
            InitializeCharacteristics();

            _playerMovement = new PlayerMovement(_joysickForMovement, this);
            _flip = new Flip();
            _move = new Move();
            _playerView.Initialize();
            _characteristicsView.Initialize(this);

            _healthInfo = Instantiate(_healthInfoPrefab, transform.position, Quaternion.identity);
            _healthInfo.Initialize(_dynamic);
            _healthView = Instantiate(_healthViewPrefab, transform.position, Quaternion.identity);
            _healthView.Initialize(this, _playerCharacteristics.Health, _healthInfo, this);

            _circleRadiusVisualizer.Initialize(transform);
            _radiusMovementTrigger.Initialize(transform, _rotationSprite.transform, _config.LayerEnemy, transform, _config.Speed, _config.VisibilityRadius);

            StartCoroutine(GradualHealing());
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
            _health.AddHealth(_playerCharacteristics.Health);
            _playerCharacteristics.AttackSpeed += 1;
            _playerCharacteristics.ReturnInitialAttackPosition += 0.02f;
            _playerCharacteristics.AddDamageAttack += 2;
            OnUp?.Invoke();

            _weapon.WeaponData.ExtraDamage = _playerCharacteristics.AddDamageAttack;
            _weapon.WeaponData.ReturnInitialAttackPosition = _playerCharacteristics.ReturnInitialAttackPosition;

            _characteristicsView.Show();
        }

        private void Move()
        {
            _playerMovement.Move();

            if (_weapon != null)
                _radiusMovementTrigger.MoveToTarget(_weapon.Config.RadiusAttack);

            MoveInRadiusAndRotation();
            _flip.RotateView(_joysickForMovement.VectorDirection(), _rotationSprite.transform);

            _healthView.FollowTargetHealth();
            _circleRadiusVisualizer.DrawRadius(_config.VisibilityRadius);
        }

        private void MoveInRadiusAndRotation()
        {
            if (_joysickForMovement.VectorDirection() != Vector2.zero)
            {
                _move.Rotation(_rotationSprite.transform, _joysickForMovement.VectorDirection());
                _radiusMovementTrigger.StopRadiusMovement();
            }
            else if (_joysickForMovement.VectorDirection() == Vector2.zero)
            {
                _radiusMovementTrigger.StartRadiusMovement();
            }
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
