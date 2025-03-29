using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon.Interface;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon
{
    public class WeaponFactory
    {
        private WeaponConfig _firstWeaponConfig;
        private Player.Player _player;
        private Transform _point;

        public WeaponFactory(WeaponConfig firstWeaponConfig, Player.Player player, Transform point)
        {
            _firstWeaponConfig = firstWeaponConfig;
            _player = player;
            _point = point;
        }

        public Weapons.Weapon Get(WeaponTypes weaponType, Vector3 position)
        {
            WeaponConfig config = GetConfigBy(weaponType);
            Weapons.Weapon instance = UnityEngine.Object.Instantiate(config.Prefab, position, Quaternion.identity, null);
            Weapons.Weapon baseEnemy = InitializeObject(instance, config);
            return baseEnemy;
        }

        private WeaponConfig GetConfigBy(WeaponTypes types)
        {
            switch (types)
            {
                case WeaponTypes.MeleeAttack:
                    return _firstWeaponConfig;

                default:
                    throw new ArgumentException(nameof(types));
            }
        }

        private Weapons.Weapon InitializeObject(Weapons.Weapon instance, WeaponConfig config)
        {
            if (instance is IMeleeAttack)
            {
                instance.Initialize(config, _player, _point);
                return instance;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}
