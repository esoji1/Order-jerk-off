using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Scripts.Weapon.Interface;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.UseWeapons
{
    public class UseWeapons : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private WeaponFactoryBootstrap _weaponFactoryBootstrap;

        private SetWeaponPoint _setWeaponPoint;

        private IBaseWeapon _baseWeapon;
        private Weapon.Weapons.Weapon _weapon;

        public event Action<Weapon.Weapons.Weapon> OnChangeWeapon;

        public Weapon.Weapons.Weapon Weapon => _weapon;

        private void Awake()
        {
            _setWeaponPoint = new SetWeaponPoint();
        }

        private void Update()
        {
            if (_baseWeapon != null)
                _baseWeapon.Attack();
        }

        private void OnTransformChildrenChanged()
        {
            GetWeapon();
        }

        public void SetWeapon(Enum weaponType)
        {
            if (weaponType.Equals(WeaponTypes.WoodenSwordPlayer))
            {
                SetWeaponParent(WeaponTypes.WoodenSwordPlayer);
            }
            else if (weaponType.Equals(WeaponTypes.WoodenAxePlayer))
            {
                SetWeaponParent(WeaponTypes.WoodenAxePlayer);
            }
        }

        public void ActiveSelfWeapon(bool value) =>
            _weapon.gameObject.SetActive(value);

        private void SetWeaponParent(WeaponTypes weaponType)
        {
            _baseWeapon = null;
            Destroy(_weapon.gameObject);
            _weapon = null;
            Weapon.Weapons.Weapon weapon = _weaponFactoryBootstrap.Factory.Get(weaponType, transform.position, transform);
            _setWeaponPoint.SetParent(weapon.transform, transform);
            _setWeaponPoint.Set(weapon.transform);
        }

        private void GetWeapon()
        {
            AttackWeaponFectory attackWeaponFectory = GetComponentInChildren<Weapon.AttackWeaponFectory>();
            attackWeaponFectory.Initialize(_player.PointRotation.transform);
            _baseWeapon = attackWeaponFectory.BaseWeapon;
            _weapon = GetComponentInChildren<Weapon.Weapons.Weapon>();
            OnChangeWeapon?.Invoke(_weapon);
        }
    }
}
