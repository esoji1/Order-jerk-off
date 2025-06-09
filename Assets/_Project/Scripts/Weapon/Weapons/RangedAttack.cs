using _Project.Core.Projectile;
using _Project.ScriptableObjects.Configs;
using _Project.Weapon.Interface;
using TMPro;
using UnityEngine;

namespace _Project.Weapon.Weapons
{
    public class RangedAttack : Weapon, IRangedAttack
    {
        private Projectile _projectile;

        public Projectile Projectile => _projectile;

        public void Initialize(WeaponConfig config, Transform pointRotation, Canvas canvas, TextMeshProUGUI textDamage, Player.Player player, Projectile projectile)
        {
            base.Initialize(config, pointRotation, canvas, textDamage, player);
            _projectile = projectile;
        }
    }
}