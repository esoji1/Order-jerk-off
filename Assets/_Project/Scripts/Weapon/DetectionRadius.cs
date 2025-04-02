using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon
{
    public class DetectionRadius
    {
        private Transform _transform;
        private LayerMask _layer;

        private List<Collider2D> _enemiesDetected = new();

        public IReadOnlyList<Collider2D> EnemiesDetected => _enemiesDetected;
        public bool HasEnemies => _enemiesDetected.Count > 0;

        public DetectionRadius(Transform transform, LayerMask layer)
        {
            _transform = transform;
            _layer = layer;
        }

        public void Detection(float visibilityRadius)
        {
            _enemiesDetected.Clear();

            Collider2D[] hits = Physics2D.OverlapCircleAll(_transform.position, visibilityRadius, _layer);
            _enemiesDetected.AddRange(hits);
        }

        public Collider2D GetNearestEnemy()
        {
            if (HasEnemies == false)
                return null;

            Collider2D nearest = null;
            float minDistance = float.MaxValue;

            foreach (Collider2D enemy in _enemiesDetected)
            {
                if (enemy == null || !enemy.gameObject.activeInHierarchy)
                    continue;

                float distance = Vector2.Distance(_transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemy;
                }
            }

            return nearest;
        }
    }
}
