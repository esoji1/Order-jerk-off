using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.SelectionGags;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Scripts.Weapon.Interface;
using Assets._Project.Sctipts.Core.HealthSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy.Enemys
{
    public class HeavyCommonEnemy : Enemy
    {
        private IBaseWeapon _baseWeapon;

        protected override IBaseWeapon BaseWeapon => _baseWeapon;

        public override void Initialize(EnemyConfig config, BattleZone battleZone, Experience prefab, HealthInfo healthInfoPrefab, HealthView healthViewPrefab,
            Canvas dynamic, LayerMask layer, WeaponFactoryBootstrap weaponFactoryBootstrap)
        {
            base.Initialize(config, battleZone, prefab, healthInfoPrefab, healthViewPrefab, dynamic, layer, weaponFactoryBootstrap);

            Weapon.Weapons.Weapon weapon = WeaponFactoryBootstrap.Factory.Get(WeaponTypes.WoodenAxeEnemy, transform.position, PointAttack.transform);

            AttackWeaponFectory attackWeaponFectory = weapon.GetComponentInChildren<AttackWeaponFectory>();
            attackWeaponFectory.Initialize(PointRotation.transform);
            _baseWeapon = attackWeaponFectory.BaseWeapon;

            SetWeaponPoint.SetParent(attackWeaponFectory.transform, PointAttack.transform);
            SetWeaponPoint.Set(attackWeaponFectory.transform);
        }
    }
}