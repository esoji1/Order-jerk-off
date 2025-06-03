using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TestingHeavyAttack : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 1f;
    [SerializeField] private float _attackDuration = 1f;
    [SerializeField] private float _returnDelay = 1.5f;

    private bool _isMoving = true;
    private Vector3 _originalPosition;
    private Coroutine _attackCoroutine;

    private void Update()
    {
        if (_isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        float distanceToTarget = Vector2.Distance(transform.position, _targetTransform.position);

        if (distanceToTarget > _stoppingDistance)
        {
            float step = _moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _targetTransform.position, step);
        }
        else
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        _isMoving = false; 
        _originalPosition = transform.position;

        yield return transform.DOMove(_targetTransform.position, _attackDuration).SetEase(Ease.OutQuad).WaitForCompletion();

        yield return new WaitForSeconds(_returnDelay);

        yield return transform.DOMove(_originalPosition, _attackDuration).WaitForCompletion();

        _isMoving = true;
        _attackCoroutine = null;
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        DOTween.Kill(transform);
    }
}
