using _Project.Core.Projectile;
using _Project.Weapon.Interface;
using System.Collections;
using UnityEngine;

namespace _Project.Weapon.Attacks
{
    public class RangedAttack : IBaseWeapon
    {
        private Weapons.RangedAttack _weapon;
        private Transform _raycastDirection;

        private DetectionRadius _enemyDetectionRadius;
        private DroppedDamage.DroppedDamage _droppedDamage;
        private SpawnProjectile _spawnProjectile;

        private Coroutine _attackCoroutine;
        private Collider2D _nearestEnemy;

        public RangedAttack(Weapons.RangedAttack weapon, Transform raycastDirection)
        {
            _weapon = weapon;
            _raycastDirection = raycastDirection;
            _enemyDetectionRadius = new DetectionRadius(_weapon.transform, _weapon.Config.Layer);
            _droppedDamage = new DroppedDamage.DroppedDamage(_weapon.TextDamage, _weapon.Canvas);
            _spawnProjectile = new SpawnProjectile();
        }

        public void Attack()
        {
            _enemyDetectionRadius.Detection(_weapon.Config.VisibilityRadius);
            _nearestEnemy = _enemyDetectionRadius.GetNearestEnemy();

            if (_nearestEnemy == null)
            {
                return;
            }

            if (WithinAttackRadius(_weapon.Config.RadiusAttack))
            {
                StartAttack();
            }
            else
            {
                StopAttack();
            }
        }

        private void StartAttack()
        {
            if (_attackCoroutine == null)
                _attackCoroutine = _weapon.StartCoroutine(Shoot());
        }

        private void StopAttack()
        {
            if (_attackCoroutine != null)
            {
                _weapon.StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds((_weapon.Config.ReturnInitialAttackPosition - _weapon.WeaponData.ReturnInitialAttackPosition)
                    - _weapon.ImprovementWeaponData.ReturnInitialAttackPosition);

                if (_nearestEnemy != null)
                {
                    Vector2 direction = (_nearestEnemy.transform.position - _weapon.transform.position).normalized;
                    GameObject bulletGameObject = _spawnProjectile.ProjectileSpawnPoint(_weapon.Projectile.gameObject, direction, _weapon.transform);
                    Projectile bullet = bulletGameObject.GetComponent<Projectile>();
                    bullet.Initialize(direction, bullet, _weapon.Config.MinDamage, _weapon.Config.MaxDamage, _weapon.WeaponData.ExtraDamage
                        + _weapon.ImprovementWeaponData.Damage, _droppedDamage, _nearestEnemy);
                }
            }
        }

        private bool WithinAttackRadius(float radiusAttack)
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(_raycastDirection.position, radiusAttack, _weapon.Config.Layer);

            foreach (Collider2D collider in collider2D)
            {
                if (collider == _nearestEnemy)
                    return true;
            }

            return false;
        }
    }
}