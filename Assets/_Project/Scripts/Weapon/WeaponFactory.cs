using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon.Interface;
using Assets._Project.Sctipts.Core;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon
{
    public class WeaponFactory
    {
        public Weapons.Weapon Get(WeaponTypes weaponType, Vector3 position, Transform point)
        {
            WeaponConfig config = GetConfigBy(weaponType);
            Weapons.Weapon instance = UnityEngine.Object.Instantiate(config.Prefab, position, Quaternion.identity, null);
            Weapons.Weapon baseWeapon = InitializeObject(instance, config, point);
            return baseWeapon;
        }

        private WeaponConfig GetConfigBy(WeaponTypes types)
        {
            switch (types)
            {
                case WeaponTypes.WoodenSwordPlayer:
                    return WeaponConfigs.WoodenSwordPlayerConfig;

                case WeaponTypes.WoodenSwordEnemy:
                    return WeaponConfigs.WoodenSwordEnemyConfig;

                case WeaponTypes.WoodenAxePlayer:
                    return WeaponConfigs.WoodenAxePlayerConfig;

                case WeaponTypes.WoodenAxeEnemy:
                    return WeaponConfigs.WoodenAxeEnemyConfig;

                default:
                    throw new ArgumentException(nameof(types));
            }
        }

        private Weapons.Weapon InitializeObject(Weapons.Weapon instance, WeaponConfig config, Transform point)
        {
            if (instance is IMeleeAttack)
            {
                instance.Initialize(config, point);
                return instance;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}
