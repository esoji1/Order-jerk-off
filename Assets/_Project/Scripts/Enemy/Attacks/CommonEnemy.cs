using Assets._Project.Scripts.Enemy.Interface;
using UnityEngine;
using System.Collections;
using Assets._Project.Scripts.Weapon;

namespace Assets._Project.Scripts.Enemy.Attacks
{
    public class CommonEnemy : IBaseEnemy
    {
        private const int AttackSpeed = 20;

        private Enemy _enemy;

        private AttackMeleeView _attackMeleeView;
        private ChangeEnemyPosition _changeEnemyPosition;

        private bool _isAttacking;
        private Coroutine _attackCoroutine;
        private Player.Player _target;
        private Vector3 _randomOffset;

        public CommonEnemy(Enemy enemy)
        {
            _enemy = enemy;
            _attackMeleeView = new AttackMeleeView();
            _changeEnemyPosition = new ChangeEnemyPosition();

            _enemy.StartCoroutine(_changeEnemyPosition.SetRandomPosition(1));
        }

        public void Attack()
        {
            //DetectPlayer();

            //if (_target != null)
            //{
            //    float distance = Vector2.Distance(_enemy.transform.position, _target.transform.position);
            //    if (distance > _enemy.Config.AttackRadius)
            //    {
            //        StopAttack();
            //        MoveToTarget();
            //    }
            //    else
            //    {
            //        StartAttack();
            //    }
            //}
        }

        private void MoveToTarget()
        {
            Vector2 targetPosition = _target.transform.position + _changeEnemyPosition.AddRandomPositionToGo;
            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, targetPosition, _enemy.Config.Speed * Time.deltaTime);
        }

        private void DetectPlayer()
        {
            Collider2D[] playerColliders = Physics2D.OverlapCircleAll(_enemy.transform.position, _enemy.Config.VisibilityRadius, _enemy.LayerMask);

            if (playerColliders.Length > 0)
                _target = playerColliders[0].GetComponent<Player.Player>();
            else
                _target = null;
        }

        private void StartAttack()
        {
            if (_isAttacking == false)
            {
                _isAttacking = true;
                _attackCoroutine = _enemy.StartCoroutine(AttackRoutine());
            }
        }

        private void StopAttack()
        {
            if (_isAttacking)
            {
                _isAttacking = false;
                if (_attackCoroutine != null)
                {
                    _enemy.StartCoroutine(_attackMeleeView.StartAttack(_enemy.PointAttack.transform, 0, AttackSpeed));
                    _enemy.StopCoroutine(_attackCoroutine);
                    _attackCoroutine = null;
                }
            }
        }

        private IEnumerator AttackRoutine()
        {
            while (_isAttacking && _target != null)
            {
                yield return new WaitForSeconds(1f);

                yield return PerformAttack(-90);

                if (_isAttacking == false)
                    yield break;

                yield return ReturnOriginalPosition(0);
            }

            _isAttacking = false;
            _attackCoroutine = null;
        }

        private IEnumerator PerformAttack(float angle)
        {
            _enemy.StartCoroutine(_attackMeleeView.StartAttack(_enemy.PointAttack.transform, angle, AttackSpeed));
            ApplyDamageEnemy();
            yield return new WaitForSeconds(_enemy.Config.ReturnInitialAttackPosition);
        }

        private IEnumerator ReturnOriginalPosition(float angle)
        {
            _enemy.StartCoroutine(_attackMeleeView.StartAttack(_enemy.PointAttack.transform, angle, AttackSpeed));
            yield return new WaitForSeconds(_enemy.Config.ReturnInitialAttackPosition);
        }

        private void ApplyDamageEnemy()
        {
            _target.Damage(_enemy.Config.Damage);
        }
    }
}