using _Project.Enemy;
using _Project.Player;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(AgentMovement))]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private FieldOfViewAttack _fov;
    [SerializeField] private int _damage; 

    private AgentMovement _agentMovement;
    private EnemyView _enemyView;

    private Coroutine _coroutine;

    private void Awake()
    {
        _agentMovement = GetComponent<AgentMovement>();
        _enemyView = GetComponentInChildren<EnemyView>();
        _enemyView.Initialize();

        _fov.OnPlayerAttack += StartAttack;
        _fov.OnPlayerStopAttack += StopAttack;
    }

    private void OnDestroy()
    {
        _fov.OnPlayerAttack -= StartAttack;
        _fov.OnPlayerStopAttack -= StopAttack;
    }

    private void StartAttack(Player target)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Attack(target));
        }
    }

    private void StopAttack(Player target)
    {
        if (_coroutine != null)
        {
            _enemyView.StopAttack();
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator Attack(Player target)
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
