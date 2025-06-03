using _Project.ConstructionBuildings.Buildings;
using _Project.Core;
using _Project.Core.Interface;
using _Project.Enemy.Attakcs.Interface;
using _Project.Enemy.Enemys;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy.Attakcs
{
    public class HeavyAttack : IBaseAttack
    {
        private const int MaximumNumberHits = 5;

        private HeavyBlowEnemy _enemy;

        private CreatingPrimitive _creatingPrimitive;

        private Coroutine _coroutine;
        private bool _isAttack;
        private Collider2D _targetDamage;
        private bool _isMove;
        private Tween _tween;
        private Tween _tweenMoveAttack;
        private int _currentNumberHits;
        private Vector3 _originalPosition;

        public HeavyAttack(Enemys.Enemy enemy)
        {
            _enemy = enemy as HeavyBlowEnemy;
            _creatingPrimitive = new CreatingPrimitive(_enemy.CirclePrimitiveHeavyAttack);
            _isMove = true;
        }

        public void Update()
        {
            if (_enemy.IsDie)
            {
                return;
            }

            _enemy.HealthView.FollowTargetHealth();

            if (_enemy.IsSleeps)
            {
                StopHeavyBlowAttack();
                return;
            }

            Move();
        }

        public void StartNormalStrikeAttack()
        {
            if (_coroutine == null)
            {
                _isAttack = true;
                _isMove = false;
                _coroutine = _enemy.StartCoroutine(NormalStrikeAttack());
            }
        }

        private void StopNormalStrikeAttack()
        {
            if (_coroutine != null)
            {
                _isAttack = false;
                _enemy.EnemyView.StopAttack();
                _isMove = true;
                _enemy.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        public void StartHeavyBlowAttack()
        {
            if (_coroutine == null)
            {
                _isAttack = true;
                _isMove = false;
                _coroutine = _enemy.StartCoroutine(HeavyBlowAttack());
            }
        }

        private void StopHeavyBlowAttack()
        {
            if (_coroutine != null)
            {
                _isAttack = false;
                _enemy.EnemyView.StopAttack();
                _isMove = true;
                _enemy.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator HeavyBlowAttack()
        {
            while (_isAttack && _enemy.IsDie == false)
            {
                if (CheckAttackHitRadius())
                {
                    _enemy.EnemyView.StartAttack();
                    _creatingPrimitive.CreatePrimitive(_targetDamage.transform, 1);
                    _tween = _creatingPrimitive.SpriteRenderer.DOFade(1, 5f);

                    yield return new WaitForSeconds(5f);

                    if (CheckHit() && _enemy.Player.IsInvisible == false)
                        DamageTarget(_enemy.Config.Damage);

                    _currentNumberHits = 0;

                    _enemy.EnemyView.StopAttack();

                    yield return new WaitForSeconds(1);

                    if (_creatingPrimitive.CreatedPrimitive != null)
                        GameObject.Destroy(_creatingPrimitive.CreatedPrimitive);

                    StopHeavyBlowAttack();
                }
                else
                {
                    yield return null;
                }
            }
        }

        private IEnumerator NormalStrikeAttack()
        {
            while (_isAttack && _enemy.IsDie == false)
            {
                if (CheckAttackHitRadius() && _currentNumberHits < MaximumNumberHits)
                { 
                    _originalPosition = _enemy.transform.position;
                    _tweenMoveAttack = _enemy.transform.DOMove(_targetDamage.transform.position, 0.5f);
                    yield return _tweenMoveAttack.WaitForCompletion();

                    if (CheckAttackHitRadius() && _enemy.Player.IsInvisible == false)
                        DamageTarget(20);

                    _currentNumberHits++;
                    yield return new WaitForSeconds(1.5f);

                    _tweenMoveAttack = _enemy.transform.DOMove(_originalPosition, 0.5f);
                    yield return _tweenMoveAttack.WaitForCompletion();
                }
                else
                {
                    StopNormalStrikeAttack();
                }
            }
        }

        private bool CheckAttackHitRadius()
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(_enemy.transform.position, _enemy.Config.AttackRadius, _enemy.Layer);

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out Player.Player _) || collider.TryGetComponent(out BaseBuilding _))
                {
                    _targetDamage = collider;
                    return true;
                }
            }

            return false;
        }

        private bool CheckHit()
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(_creatingPrimitive.CreatedPrimitive.transform.position,
               _enemy.Config.AttackRadius, _enemy.Layer);

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out Player.Player _) || collider.TryGetComponent(out BaseBuilding _))
                {
                    _targetDamage = collider;
                    return true;
                }
            }

            return false;
        }

        private void DamageTarget(int damage)
        {
            if (_targetDamage == null)
                return;

            _targetDamage.GetComponent<IDamage>().Damage(damage);
        }

        private void Move()
        {
            if (_enemy.Player.IsInvisible == false)
            {
                if (CheckAttackHitRadius())
                {
                    if (_currentNumberHits < MaximumNumberHits)
                        StartNormalStrikeAttack();
                    else if (_currentNumberHits >= MaximumNumberHits)
                        StartHeavyBlowAttack();
                }
                if (_isMove)
                {
                    if (_enemy.RadiusMovementTrigger.MoveToTarget(_enemy.Config.AttackRadius, _enemy.Config.VisibilityRadius))
                    {
                        _enemy.Agent.isStopped = true;
                        return;
                    }
                }
            }
            if (_isMove)
            {
                if (_enemy.IsMoveRandomPoints)
                {
                    _enemy.RandomMovePoints.MovePoints();
                }
            }
            if (_isMove == false)
            {
                _enemy.Agent.isStopped = true;
            }
            if (_enemy.IsMoveRandomPoints == false)
            {
                _enemy.MoveToPoint.MovePoints();
            }
        }

        public void OnDestroy()
        {
            _tween.Kill();
            _tweenMoveAttack.Kill();

            if (_creatingPrimitive.CreatedPrimitive != null)
                GameObject.Destroy(_creatingPrimitive.CreatedPrimitive);
        }
    }
}
