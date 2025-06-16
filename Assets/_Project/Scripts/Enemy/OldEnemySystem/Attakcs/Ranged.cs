//using _Project.Enemy.Attakcs.Interface;
//using _Project.Weapon;
//using _Project.Weapon.Projectile;
//using System.Collections;
//using UnityEngine;

//namespace _Project.Enemy.Attakcs
//{
//    public class Ranged : IBaseAttack
//    {
//        private Enemys.MagicianEnemy _enemy;

//        private DetectionRadius _enemyDetectionRadius;
//        private SpawnProjectile _spawnProjectile;

//        private Coroutine _coroutine;
//        private Collider2D _nearestEnemy;

//        public Ranged(Enemys.Enemy enemy)
//        {
//            _enemy = enemy as Enemys.MagicianEnemy;
//            _enemyDetectionRadius = new DetectionRadius(_enemy.transform, _enemy.Layer);
//            _spawnProjectile = new SpawnProjectile();
//        }

//        public void Update()
//        {
//            if (_enemy.IsDie)
//            {
//                return;
//            }

//            _enemy.HealthView.FollowTargetHealth();

//            if(_enemy.IsSleeps)
//            {
//                StopAttack();
//                return;
//            }

//            Move();

//            _enemyDetectionRadius.Detection(_enemy.Config.VisibilityRadius);
//            _nearestEnemy = _enemyDetectionRadius.GetNearestEnemy();

//            if (_nearestEnemy == null)
//            {
//                return;
//            }

//            if (WithinAttackRadius(_enemy.Config.VisibilityRadius) && _enemy.Player.IsInvisible == false)
//            {
//                StartAttack();
//            }
//            else
//            {
//                StopAttack();
//            }
//        }

//        private bool WithinAttackRadius(float radiusAttack)
//        {
//            Collider2D[] collider2D = Physics2D.OverlapCircleAll(_enemy.transform.position, radiusAttack, _enemy.Layer);

//            foreach (Collider2D collider in collider2D)
//            {
//                if (collider == _nearestEnemy)
//                    return true;
//            }

//            return false;
//        }

//        private void StartAttack()
//        {
//            if (_coroutine == null)
//                _coroutine = _enemy.StartCoroutine(Shoot());
//        }

//        private void StopAttack()
//        {
//            if (_coroutine != null)
//            {
//                _enemy.StopCoroutine(_coroutine);
//                _coroutine = null;
//            }
//        }

//        private IEnumerator Shoot()
//        {
//            while (true && _enemy.IsDie == false)
//            {
//                yield return new WaitForSeconds(1f);

//                if (_nearestEnemy != null && _enemy.Player.IsInvisible == false)
//                {
//                    Vector2 direction = (_nearestEnemy.transform.position - _enemy.transform.position).normalized;
//                    GameObject bulletGameObject = _spawnProjectile.ProjectileSpawnPoint(_enemy.ProjectileEnemy.gameObject, direction, _enemy.transform);
//                    ProjectileEnemy bullet = bulletGameObject.GetComponent<ProjectileEnemy>();
//                    bullet.Initialize(direction, bullet, _enemy.Config.Damage, _enemy.Config.Damage, 0);
//                }
//            }
//        }

//        private void Move()
//        {
//            if (_enemy.Player.IsInvisible == false)
//            {
//                if (_enemy.RadiusMovementTrigger.MoveToTarget(_enemy.Config.AttackRadius, _enemy.Config.VisibilityRadius))
//                {
//                    _enemy.Agent.isStopped = true;
//                    return;
//                }
//            }

//            if (_enemy.IsMoveRandomPoints)
//            {
//                _enemy.RandomMovePoints.MovePoints();
//            }

//            if (_enemy.IsMoveRandomPoints == false)
//            {
//                _enemy.MoveToTarget.MovePoints();
//            }
//        }

//        public void OnDestroy()
//        {
//        }
//    }
//}