using Assets._Project.Scripts.Core;
using Assets._Project.Sctipts.Core;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon
{
    public class WeaponFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _pointWeapon;
        [SerializeField] private PointAttack _pointAttack;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TextMeshProUGUI _textDamage;

        private WeaponFactory _factory;
        private Weapons.Weapon weapon;
        private SetWeaponPoint _setWeaponPoint;

        public SetWeaponPoint SetWeaponPoint => _setWeaponPoint;
        public WeaponFactory Factory => _factory;

        private void Awake()
        {
            _factory = new WeaponFactory(_canvas, _textDamage);
            _setWeaponPoint = new SetWeaponPoint();
        }

        private void Start()
        {
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