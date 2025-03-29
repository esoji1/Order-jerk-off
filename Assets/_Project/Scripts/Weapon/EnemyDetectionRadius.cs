using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon
{
    public class EnemyDetectionRadius
    {
        private readonly Player.Player _player;
        private readonly List<Collider2D> _enemiesDetected = new();

        public IReadOnlyList<Collider2D> EnemiesDetected => _enemiesDetected;
        public bool HasEnemies => _enemiesDetected.Count > 0;

        public EnemyDetectionRadius(Player.Player player)
        {
            _player = player;
        }

        public void Detection()
        {
            _enemiesDetected.Clear();

            Collider2D[] hits = Physics2D.OverlapCircleAll(_player.transform.position, _player.Config.AttackRadius, _player.Config.LayerEnemy);
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

                float distance = Vector2.Distance(_player.transform.position, enemy.transform.position);
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
