//using _Project.ConstructionBuildings.Buildings;
//using _Project.Core.Interface;
//using _Project.Enemy.Attakcs.Interface;
//using System.Collections;
//using UnityEngine;

//namespace _Project.Enemy.Attakcs
//{
//    public class MeleeNormalAttack : IBaseAttack
//    {
//        private Enemys.Enemy _enemy;

//        private Coroutine _coroutine;
//        private bool _isAttack;
//        private Collider2D _targetDamage;
//        private Vector3 _previousPosition;
//        private Vector3 _smoothedDirection;

//        public MeleeNormalAttack(Enemys.Enemy enemy)
//        {
//            _enemy = enemy;
//        }

//        public void Update()
//        {
//            if (_enemy.IsDie)
//            {
//                return;
//            }

//            _enemy.HealthView.FollowTargetHealth();

//            if (_enemy.IsSleeps)
//            {
//                StopAttackIfNeeded();
//                return;
//            }


//            Move();
//        }

//        public void StartAttackIfNeeded()
//        {
//            if (_coroutine == null)
//            {
//                _isAttack = true;
//                _coroutine = _enemy.StartCoroutine(Attack());
//            }
//        }

//        private void StopAttackIfNeeded()
//        {
//            if (_coroutine != null)
//            {
//                _isAttack = false;
//                _enemy.EnemyView.StopAttack();
//                _enemy.StopCoroutine(_coroutine);
//                _coroutine = null;
//            }
//        }

//        private IEnumerator Attack()
//        {
//            while (_isAttack && _enemy.IsDie == false)
//            {
//                if (CheckAttackHitRadius())
//                {
//                    _enemy.EnemyView.StartAttack();

//                    float attackAnimationTime = _enemy.EnemyView.Animator.GetCurrentAnimatorStateInfo(0).length;
//                    yield return new WaitForSeconds(attackAnimationTime);

//                    if (CheckAttackHitRadius() && _enemy.Player.IsInvisible == false)
//                        DamageTarget();

//                    _enemy.EnemyView.StopAttack();

//                    yield return new WaitForSeconds(1);
//                }
//                else
//                {
//                    StopAttackIfNeeded();
//                }
//            }
//        }

//        private bool CheckAttackHitRadius()
//        {
//            Collider2D[] collider2D = Physics2D.OverlapCircleAll(_enemy.transform.position, _enemy.Config.AttackRadius, _enemy.Layer);

//            foreach (Collider2D collider in collider2D)
//            {
//                if (collider.TryGetComponent(out Player.Player _) || collider.TryGetComponent(out BaseBuilding _))
//                {
//                    _targetDamage = collider;
//                    return true;
//                }
//            }

//            return false;
//        }

//        private void DamageTarget()
//        {
//            if (_targetDamage == null)
//                return;

//            _targetDamage.GetComponent<IDamage>().Damage(_enemy.Config.Damage);
//        }

//        private void Move()
//        {
//            Vector3 currentDirection = (_enemy.transform.position - _previousPosition).normalized;

//            _previousPosition = _enemy.transform.position;

//            _smoothedDirection = Vector3.Lerp(_smoothedDirection, currentDirection, Time.deltaTime * 10f);

//            _enemy.EnemyView.UpdateRunX(_smoothedDirection.x);
//            _enemy.EnemyView.UpdateRunY(_smoothedDirection.y);

//            if (_enemy.Player.IsInvisible == false)
//            {
//                if (CheckAttackHitRadius())
//                {
//                    StartAttackIfNeeded();
//                }

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
//                _enemy.MoveToPoint.MovePoints();
//            }
//        }

//        public void OnDestroy()
//        {
//        }
//    }
//}
