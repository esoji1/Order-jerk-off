using _Project.ScriptableObjects.Configs;
using _Project.Weapon.Interface;
using TMPro;
using UnityEngine;

namespace _Project.Weapon.Weapons
{
    public class RangedAttack : Weapon, IRangedAttack
    {
        private Projectile.Projectile _projectile;

        public Projectile.Projectile Projectile => _projectile;

        public void Initialize(WeaponConfig config, Transform pointRotation, Canvas canvas, TextMeshProUGUI textDamage, Player.Player player, Projectile.Projectile projectile)
        {
            base.Initialize(config, pointRotation, canvas, textDamage, player);
            _projectile = projectile;
        }
    }
}