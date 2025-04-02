using Assets._Project.Scripts.Core.Interface;
using Assets._Project.Scripts.Weapon.Interface;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon.Attacks
{
    public class MeleeAttack : IBaseWeapon
    {
        private Weapons.Weapon _weapon;

        private DetectionRadius _enemyDetectionRadius;
        private AttackMeleeView _attackMeleeView;

        private Coroutine _attackCoroutine;
        private bool _isAttacking;
        private Collider2D _nearestEnemy;

        public MeleeAttack(Weapons.Weapon weapon)
        {
            _weapon = weapon;
            _enemyDetectionRadius = new DetectionRadius(_weapon.transform, _weapon.Config.Layer);
            _attackMeleeView = new AttackMeleeView();
        }

        public void Attack()
        {
            Debug.DrawRay(_weapon.transform.position, _weapon.transform.right * _weapon.Config.RaycastAttack, Color.green);

            _enemyDetectionRadius.Detection(_weapon.Config.VisibilityRadius);
            _nearestEnemy = _enemyDetectionRadius.GetNearestEnemy();

            if (_nearestEnemy == null)
            {
                StopAttack();
                return;
            }

            float distance = Vector2.Distance(_weapon.transform.position, _nearestEnemy.transform.position) - 0.1f;

            if (distance < _weapon.Config.RadiusAttack)
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
                _weapon.StartCoroutine(ReturnOriginalPosition(0));
            }
        }

        private IEnumerator AttackRoutine()
        {
            while (_isAttacking && _enemyDetectionRadius.EnemiesDetected.Count > 0)
            {
                if (CheckAttackHit())
                {
                    yield return PerformAttack(-90);

                    yield return ReturnOriginalPosition(0);
                }
                else
                {
                    StopAttack();
                    yield break;
                }
            }

            _isAttacking = false;
            _attackCoroutine = null;
        }

        private IEnumerator PerformAttack(float angle)
        {
            _weapon.StartCoroutine(_attackMeleeView.StartAttack(_weapon.Point, angle, _weapon.Config.AttackSpeed));
            ApplyDamageEnemy();
            yield return new WaitForSeconds(_weapon.Config.ReturnInitialAttackPosition - _weapon.WeaponData.ReturnInitialAttackPosition);
        }

        private IEnumerator ReturnOriginalPosition(float angle)
        {
            _weapon.StartCoroutine(_attackMeleeView.StartAttack(_weapon.Point, angle, _weapon.Config.AttackSpeed));
            yield return new WaitForSeconds(_weapon.Config.ReturnInitialAttackPosition - _weapon.WeaponData.ReturnInitialAttackPosition);
        }

        private bool CheckAttackHit()
        {
            Vector2 direction = _weapon.transform.right;
            RaycastHit2D hit = Physics2D.Raycast(_weapon.transform.position, direction, _weapon.Config.RaycastAttack, _weapon.Config.Layer);

            return hit.collider != null && hit.collider.TryGetComponent(out IDamage _);
        }

        private void ApplyDamageEnemy()
        {
            if (_nearestEnemy.TryGetComponent(out IDamage damage))
                damage.Damage(_weapon.Config.Damage + _weapon.WeaponData.ExtraDamage);
        }
    }
}