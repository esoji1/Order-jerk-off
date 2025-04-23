using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon.Interface;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon.Weapons
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