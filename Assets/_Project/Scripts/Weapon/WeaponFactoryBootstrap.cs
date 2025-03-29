using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon
{
    public class WeaponFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _firstWeaponConfig;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Transform _pointWeapon;

        private WeaponFactory _factory;
        private Weapons.Weapon weapon;
        private SetWeaponPoint _setWeaponPoint;

        public SetWeaponPoint SetWeaponPoint => _setWeaponPoint;

        private void Awake()
        {
            _factory = new WeaponFactory(_firstWeaponConfig, _player, _pointWeapon);
            _setWeaponPoint = new SetWeaponPoint();

            SpawnStandartWeapon();
        }

        private void SpawnStandartWeapon()
        {
            weapon = _factory.Get(WeaponTypes.MeleeAttack, transform.position);
            weapon.transform.SetParent(_pointWeapon);
            _setWeaponPoint.Set(weapon.transform);
        }
    }
}