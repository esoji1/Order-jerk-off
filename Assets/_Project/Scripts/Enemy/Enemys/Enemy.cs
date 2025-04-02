using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Core.HealthSystem;
using Assets._Project.Scripts.Core.Interface;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Scripts.Weapon.Interface;
using Assets._Project.Sctipts.Core;
using Assets._Project.Sctipts.Core.HealthSystem;
using Assets._Project.Sctipts.Core.Spawns;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    [RequireComponent(typeof(RadiusMovementTrigger))]
    public abstract class Enemy : MonoBehaviour, IDamage, IOnDamage
    {
        protected WeaponFactoryBootstrap WeaponFactoryBootstrap;
        protected PointAttack PointAttack;
        protected SetWeaponPoint SetWeaponPoint;
        protected PointRotation PointRotation;

        protected abstract IBaseWeapon BaseWeapon { get; }

        private EnemyConfig _config;
        private BattleZone _battleZone;
        private SelectionGags.Experience _prefab;
        private HealthInfo _healthInfoPrefab;
        private HealthView _healthViewPrefab;
        private Canvas _dynamic;
        private LayerMask _layer;

        private PointExperience _pointExperience;
        private PointHealth _pointHealth;
        private PointCoin _pointCoin;
        private RadiusMovementTrigger _radiusMovementTrigger;

        private Health _health;
        private SpawnExperience _spawnExperience;
        private HealthInfo _healthInfo;
        private HealthView _healthView;

        public event Action<int> OnDamage;

        public BattleZone BattleZone => _battleZone;
        public PointHealth PointHealth => _pointHealth;
        public EnemyConfig Config => _config;
        public LayerMask LayerMask => _layer;

        public virtual void Initialize(EnemyConfig config, BattleZone battleZone, SelectionGags.Experience prefab, HealthInfo healthInfoPrefab,
            HealthView healthViewPrefab, Canvas dynamic, LayerMask layer, WeaponFactoryBootstrap weaponFactoryBootstrap)
        {
            ExtractComponents();

            _config = config;
            _battleZone = battleZone;
            _prefab = prefab;
            _healthInfoPrefab = healthInfoPrefab;
            _healthViewPrefab = healthViewPrefab;
            _dynamic = dynamic;
            _layer = layer;
            WeaponFactoryBootstrap = weaponFactoryBootstrap;
            _health = new Health(_config.Health);
            _spawnExperience = new SpawnExperience(_prefab, _config.AmountExperienceDropped);
            SetWeaponPoint = new SetWeaponPoint();

            _healthInfo = Instantiate(_healthInfoPrefab, transform.position, Quaternion.identity);
            _healthInfo.Initialize(_dynamic);
            _healthView = Instantiate(_healthViewPrefab, transform.position, Quaternion.identity);
            _healthView.Initialize(this, _config.Health, _healthInfo, null);

            _radiusMovementTrigger.Initialize(transform, PointRotation.transform, layer, transform, _config.Speed, _config.VisibilityRadius);

            _health.OnDie += Die;
        }

        private void Update()
        {
            _healthView.FollowTargetHealth();
            _radiusMovementTrigger.MoveToTarget(_config.AttackRadius);
            BaseWeapon.Attack();
        }

        public void Damage(int damage)
        {
            _health.TakeDamage(damage);
            OnDamage?.Invoke(damage);
        }

        private void ExtractComponents()
        {
            _pointExperience = GetComponentInChildren<PointExperience>();
            _pointCoin = GetComponentInChildren<PointCoin>();
            _pointHealth = GetComponentInChildren<PointHealth>();
            PointAttack = GetComponentInChildren<PointAttack>();
            _radiusMovementTrigger = GetComponent<RadiusMovementTrigger>();
            PointRotation = GetComponentInChildren<PointRotation>();
        }

        public void Die()
        {
            _spawnExperience.Spawn(_pointExperience.transform);

            _health.OnDie -= Die;
            Destroy(_healthInfo.InstantiatedHealthBar.gameObject);
            Destroy(_healthInfo.gameObject);
            Destroy(_healthView.gameObject);
            Destroy(gameObject);
        }

        public void DieHp()
        {
            Destroy(_healthInfo.InstantiatedHealthBar.gameObject);
            Destroy(_healthInfo.gameObject);
            Destroy(_healthView.gameObject);
        }
    }
}