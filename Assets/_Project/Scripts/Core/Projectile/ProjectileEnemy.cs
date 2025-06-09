using _Project.Core.Interface;
using UnityEngine;

namespace _Project.Core.Projectile
{
    public class ProjectileEnemy : MonoBehaviour
    {
        private ProjectileEnemy _projectile;
        private Vector2 _direction;
        private int _damage;

        private void Update()
        {
            GiveBulletAcceleration();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamage damage))
            {
                if (damage is Player.Player enemy)
                {
                    enemy.Damage(_damage);
                    Destroy(_projectile.gameObject);
                }
                Destroy(_projectile.gameObject, 4f);
            }
        }

        public void Initialize(Vector2 direction, ProjectileEnemy projectile, int damage)
        {
            _direction = direction.normalized;
            _projectile = projectile;
            _damage = damage;
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
