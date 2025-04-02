using Assets._Project.Scripts.Weapon.Attacks;
using Assets._Project.Scripts.Weapon.Interface;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon
{
    public class AttackWeaponFectory : MonoBehaviour
    {
        private Weapons.Weapon _weapon;
        private IBaseWeapon _baseWeapon;

        private Transform _raycastDirection;

        public IBaseWeapon BaseWeapon => _baseWeapon;

        public void Initialize(Transform raycastDirection)
        {
            _weapon = GetComponent<Weapons.Weapon>();
            _raycastDirection = raycastDirection;
            Get();
        }

        private void Get()
        {
            switch (_weapon)
            {
                case IMeleeAttack:
                    _baseWeapon = new MeleeAttack(_weapon, _raycastDirection);
                    break;

                default:
                    throw new ArgumentException($"There is no such type of attack {_weapon}");
            }
        }
    }
}