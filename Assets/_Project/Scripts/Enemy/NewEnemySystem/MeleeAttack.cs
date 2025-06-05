using _Project.Enemy;
using _Project.Player;
using System.Collections;
using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(AgentMovement))]
    public class MeleeAttack : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private EnemyView _enemyView;
        private FieldOfViewAttack _fovViewAttack;

        private Coroutine _coroutine;

        private void Awake()
        {
            ExtractComponents();
            _enemyView.Initialize();

            _fovViewAttack.OnPlayerAttack += StartAttack;
            _fovViewAttack.OnPlayerStopAttack += StopAttack;
        }

        private void OnDestroy()
        {
            _fovViewAttack.OnPlayerAttack -= StartAttack;
            _fovViewAttack.OnPlayerStopAttack -= StopAttack;
        }

        private void ExtractComponents()
        {
            _enemyView = GetComponentInChildren<EnemyView>();
            _fovViewAttack = GetComponentInChildren<FieldOfViewAttack>();
        }

        private void StartAttack(Player.Player target)
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(Attack(target));
        }

        private void StopAttack(Player.Player target)
        {
            if (_coroutine != null)
            {
                _enemyView.StopAttack();
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator Attack(Player.Player target)
        {
            while (true)
            {
                _enemyView.StartAttack();
                float attackAnimationTime = _enemyView.Animator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(attackAnimationTime);
                target.Damage(20);
            }
        }
    }
}
