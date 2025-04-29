using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using Assets._Project.Scripts.Player;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon.Projectile;
using Assets._Project.Scripts.Weapon;
using UnityEngine;
using System.Collections;
using TMPro;

namespace Assets._Project.Scripts.ConstructionBuildings.DefensiveBuildings
{
    [RequireComponent(typeof(CircleRadiusVisualizer))]
    public class RangedAttackTower : BaseBuilding
    {
        private CircleRadiusVisualizer _circleRadiusVisualizer;
        private DetectionRadius _enemyDetectionRadius;
        private DroppedDamage.DroppedDamage _droppedDamage;
        private SpawnProjectile _spawnProjectile;
        private Projectile _projectile;
        private LayerMask _layerMask;

        private Coroutine _attackCoroutine;
        private Collider2D _nearestEnemy;

        public void Initialize(BuildsConfig config, Canvas staticCanvas, Player.Player player, Sctipts.Inventory.Inventory inventory, Canvas dynamic,
            TextMeshProUGUI textDamage, LayerMask layer, Projectile projectile)
        {
            base.Initialize(config, staticCanvas, player);

            _projectile = projectile;
            _layerMask = layer;

            _circleRadiusVisualizer = GetComponent<CircleRadiusVisualizer>();
            _circleRadiusVisualizer.Initialize(transform);

            _enemyDetectionRadius = new DetectionRadius(transform, layer);
            _droppedDamage = new DroppedDamage.DroppedDamage(textDamage, dynamic);
            _spawnProjectile = new SpawnProjectile();
        }

        private void Update()
        {
            _circleRadiusVisualizer.DrawRadius(Config.RadiusAttack);
            Attack();
        }

        public void Attack()
        {
            _enemyDetectionRadius.Detection(Config.RadiusAttack);
            _nearestEnemy = _enemyDetectionRadius.GetNearestEnemy();

            if (_nearestEnemy == null)
            {
                return;
            }

            if (WithinAttackRadius(Config.RadiusAttack))
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
                _attackCoroutine = StartCoroutine(Shoot());
        }

        private void StopAttack()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }

        private IEnumerator Shoot()
        {
            while (true)
            {

                if (_nearestEnemy != null)
                {
                    Vector2 direction = (_nearestEnemy.transform.position - transform.position).normalized;
                    GameObject bulletGameObject = _spawnProjectile.ProjectileSpawnPoint(_projectile.gameObject, direction, transform);
                    Projectile bullet = bulletGameObject.GetComponent<Projectile>();
                    bullet.Initialize(direction, bullet, Config.Damage, Config.Damage, 0, _droppedDamage, _nearestEnemy);
                }
                yield return new WaitForSeconds(Config.DelayAttack);
            }
        }

        private bool WithinAttackRadius(float radiusAttack)
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, radiusAttack, _layerMask);

            foreach (Collider2D collider in collider2D)
            {
                if (collider == _nearestEnemy)
                    return true;
            }

            return false;
        }
    }
}