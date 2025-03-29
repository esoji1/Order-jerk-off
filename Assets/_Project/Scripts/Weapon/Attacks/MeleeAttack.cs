using Assets._Project.Scripts.Weapon.Interface;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon.Attacks
{
    public class MeleeAttack : IBaseWeapon
    {
        private Weapons.Weapon _weapon;

        private EnemyDetectionRadius _enemyDetectionRadius;
        private AttackMeleeView _attackMeleeView;

        private Coroutine _attackCoroutine;
        private bool _isAttacking;
        private Collider2D _nearestEnemy;

        public MeleeAttack(Weapons.Weapon weapon)
        {
            _weapon = weapon;
            _enemyDetectionRadius = new EnemyDetectionRadius(_weapon.Player);
            _attackMeleeView = new AttackMeleeView();
        }

        public void Attack()
        {
            if (_weapon.Player.JoysickForMovement.VectorDirection() != Vector2.zero)
            {
                _weapon.StartCoroutine(ReturnOriginalPosition(0));
                StopAttack();
                return;
            }

            _enemyDetectionRadius.Detection();

            _nearestEnemy = _enemyDetectionRadius.GetNearestEnemy();
            if (_nearestEnemy == null)
            {
                StopAttack();
                return;
            }

            float distance = Vector2.Distance(_weapon.Player.transform.position, _nearestEnemy.transform.position);

            if (distance > _weapon.Config.RadiusAttack)
            {
                StopAttack();
                _weapon.Player.PlayerMovement.MoveTarget(_nearestEnemy.transform);
            }
            else
            {
                StartAttack();
            }
        }

        private void StartAttack()
        {
            if (_isAttacking == false)
            {
                _isAttacking = true;
                _attackCoroutine = _weapon.StartCoroutine(AttackRoutine());
            }
        }

        private void StopAttack()
        {
            if (_isAttacking)
            {
                _isAttacking = false;
                if (_attackCoroutine != null)
                {
                    _weapon.StopCoroutine(_attackCoroutine);
                    _attackCoroutine = null;
                }
            }
        }

        private IEnumerator AttackRoutine()
        {
            while (_isAttacking && _enemyDetectionRadius.EnemiesDetected.Count > 0)
            {
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
            _weapon.StartCoroutine(_attackMeleeView.StartAttack(_weapon.Point, angle, _weapon.Config.AttackSpeed));
            ApplyDamageEnemy();
            yield return new WaitForSeconds(_weapon.Config.ReturnInitialAttackPosition - _weapon.Player.PlayerCharacteristics.ReturnInitialAttackPosition);
        }

        private IEnumerator ReturnOriginalPosition(float angle)
        {
            _weapon.StartCoroutine(_attackMeleeView.StartAttack(_weapon.Point, angle, _weapon.Config.AttackSpeed));
            yield return new WaitForSeconds(_weapon.Config.ReturnInitialAttackPosition - _weapon.Player.PlayerCharacteristics.ReturnInitialAttackPosition);
        }

        private void ApplyDamageEnemy()
        {
            if (_nearestEnemy.TryGetComponent(out Enemy.Enemy enemy))
                enemy.Damage(_weapon.Config.Damage + _weapon.Player.PlayerCharacteristics.AddDamageAttack);
        }
    }
}