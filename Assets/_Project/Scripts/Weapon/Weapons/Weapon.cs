using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Sctipts.Core.TempData;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon.Weapons
{
    [RequireComponent(typeof(AttackWeaponFectory))]
    public abstract class Weapon : MonoBehaviour
    {
        private WeaponConfig _config;
        private Transform _pointRotation;

        private AttackMeleeView _attackMeleeView;
        private Move _move;
        private WeaponData _weaponData;

        public WeaponConfig Config => _config;
        public AttackMeleeView AttackMeleeView => _attackMeleeView;
        public Transform Point => _pointRotation;
        public Move Move => _move;
        public WeaponData WeaponData => _weaponData;

        public virtual void Initialize(WeaponConfig config, Transform pointRotation)
        {
            _config = config;
            _pointRotation = pointRotation;
            _attackMeleeView = new AttackMeleeView();
            _move = new Move();
            _weaponData = new WeaponData();
        }
    }
}