using _Project.Core;
using _Project.ScriptableObjects.Configs;
using _Project.Weapon.Interface;
using System;
using TMPro;
using UnityEngine;

namespace _Project.Weapon
{
    public class WeaponFactory
    {
        private Canvas _canvas;
        private TextMeshProUGUI _textDamage;
        private Player.Player _player;
        private Projectile.Projectile _projectile;

        public WeaponFactory(Canvas canvas, TextMeshProUGUI textDamage, Player.Player player, Projectile.Projectile projectile)
        {
            _canvas = canvas;
            _textDamage = textDamage;
            _player = player;
            _projectile = projectile;
        }

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

                case WeaponTypes.WoodenOnionPlayer:
                    return WeaponConfigs.WeaponOnionPlayerConfig;

                case WeaponTypes.WoodenOnionEnemy:
                    return WeaponConfigs.WeaponOnionEnemyConfig;

                default:
                    throw new ArgumentException(nameof(types));
            }
        }

        private Weapons.Weapon InitializeObject(Weapons.Weapon instance, WeaponConfig config, Transform point)
        {
            if (instance is IMeleeAttack)
            {
                instance.Initialize(config, point, _canvas, _textDamage, _player);
                return instance;
            }
            else if (instance is IRangedAttack)
            {
                if (instance is Weapons.RangedAttack rangedAttack)
                {
                    rangedAttack.Initialize(config, point, _canvas, _textDamage, _player, _projectile);
                    return instance;
                }
                return null;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}
