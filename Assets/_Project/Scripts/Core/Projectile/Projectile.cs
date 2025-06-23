using _Project.Core.Interface;
using UnityEngine;

namespace _Project.Core.Projectile
{
    public class Projectile : MonoBehaviour
    {
        private Projectile _projectile;
        private Vector2 _direction;
        private DroppedDamage.DroppedDamage _droppedDamage;
        private Collider2D _nearestEnemy;
        private int _minDamage;
        private int _maxDamage;
        private int _extraDamage;

        private void Update()
        {
            GiveBulletAcceleration();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamage damage))
            {
                if (damage is Enemy.Behaviors.Enemy enemy)
                {
                    int randomDamage = Random.Range(_minDamage, _maxDamage) + _extraDamage;
                    _droppedDamage.SpawnNumber(randomDamage, _nearestEnemy.transform);
                    enemy.Damage(randomDamage);
                    Destroy(_projectile.gameObject);
                }
            }
        }

        public void Initialize(Vector2 direction, Projectile projectile, int minDamage, int maxDamage, int extraDamage, DroppedDamage.DroppedDamage droppedDamage,
            Collider2D nearestEnemy)
        {
            _direction = direction.normalized;
            _projectile = projectile;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _extraDamage = extraDamage;
            _droppedDamage = droppedDamage;
            _nearestEnemy = nearestEnemy;

            Destroy(_projectile.gameObject, 4f);
        }

        private void GiveBulletAcceleration()
        {
            if (_direction != Vector2.zero)
                TranslateBullet();
        }

        private void TranslateBullet() =>
            transform.Translate(_direction * 5f * Time.deltaTime, Space.World);
    }
}