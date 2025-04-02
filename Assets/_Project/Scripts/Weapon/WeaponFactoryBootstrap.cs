using Assets._Project.Scripts.Core;
using Assets._Project.Sctipts.Core;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon
{
    public class WeaponFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _pointWeapon;
        [SerializeField] private PointAttack _pointAttack;

        private WeaponFactory _factory;
        private Weapons.Weapon weapon;
        private SetWeaponPoint _setWeaponPoint;

        public SetWeaponPoint SetWeaponPoint => _setWeaponPoint;
        public WeaponFactory Factory => _factory;

        private void Awake()
        {
            _factory = new WeaponFactory();
            _setWeaponPoint = new SetWeaponPoint();

            SpawnStandartWeapon();
        }

        private void SpawnStandartWeapon()
        {
            weapon = _factory.Get(WeaponTypes.WoodenSwordPlayer, transform.position, _pointAttack.transform);
            _setWeaponPoint.SetParent(weapon.transform, _pointWeapon);
            _setWeaponPoint.Set(weapon.transform);
        }
    }
}