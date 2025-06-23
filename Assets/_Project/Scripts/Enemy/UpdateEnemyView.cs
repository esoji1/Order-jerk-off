using _Project.Enemy;
using _Project.Enemy.EnemyView;
using UnityEngine;

namespace _Project.Enemy
{
    public class UpdateEnemyView : MonoBehaviour
    {
        private BaseEnemyView _enemyView;

        private Vector3 _previousPosition;
        private Vector3 _smoothedDirection;

        private void Awake()
        {
            _enemyView = GetComponentInChildren<BaseEnemyView>();
            _enemyView.Initialize();
        }

        private void Update() => UpdateRotationView();

        private void UpdateRotationView()
        {
            Vector3 currentDirection = (transform.position - _previousPosition).normalized;

            _previousPosition = transform.position;

            _smoothedDirection = Vector3.Lerp(_smoothedDirection, currentDirection, Time.deltaTime * 10f);

            _enemyView.UpdateRunX(_smoothedDirection.x);
            _enemyView.UpdateRunY(_smoothedDirection.y);
        }
    }
}