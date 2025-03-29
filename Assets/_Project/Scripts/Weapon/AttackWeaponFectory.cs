using Assets._Project.Scripts.Weapon.Attacks;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon
{
    public class AttackWeaponFectory : MonoBehaviour
    {
        private Weapons.Weapon _weapon;
        private Interface.IBaseWeapon _baseWeapon;

        public Interface.IBaseWeapon BaseWeapon => _baseWeapon;

        private void Start()
        {
            _weapon = GetComponent<Weapons.Weapon>();
            Get();
        }

        private void Get()
        {
            switch (_weapon)
            {
                case Interface.IMeleeAttack:
                    _baseWeapon = new MeleeAttack(_weapon);
                    break;

                //» так дальше пишешь классы и плодишь врагов

                default:
                    throw new ArgumentException($"There is no such type of attack {_weapon}");
            }
        }
    }
}