using Assets._Project.Scripts.Weapon.Interface;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.UseWeapons
{
    public class UseWeapons : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;

        private IBaseWeapon _baseWeapon;
        private Weapon.Weapons.Weapon _weapon;

        public event Action<Weapon.Weapons.Weapon> OnChangeWeapon;

        private void Update()
        {
            if (_baseWeapon != null)
                _baseWeapon.Attack();
        }

        private void OnTransformChildrenChanged()
        {
            Invoke("GetWeapon", 1f);
        }

        private void GetWeapon()
        {
            _baseWeapon = GetComponentInChildren<Weapon.AttackWeaponFectory>().BaseWeapon;
            _weapon = GetComponentInChildren<Weapon.Weapons.Weapon>();
            OnChangeWeapon?.Invoke(_weapon);
        }
    }
}
