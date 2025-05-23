using _Project.Core.Interface;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Weapon.Projectile
{
    public class ProjectileEnemy : MonoBehaviour
    {
        private ProjectileEnemy _projectile;
        private Vector2 _direction;
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
                if (damage is Player.Player enemy)
                {
                    int randomDamage = Random.Range(_minDamage, _maxDamage) + _extraDamage;
                    enemy.Damage(randomDamage);
                    Destroy(_projectile.gameObject);
                }
                Destroy(_projectile.gameObject, 4f);
            }
        }

        public void Initialize(Vector2 direction, ProjectileEnemy projectile, int minDamage, int maxDamage, int extraDamage)
        {
            _direction = direction.normalized;
            _projectile = projectile;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _extraDamage = extraDamage;
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
