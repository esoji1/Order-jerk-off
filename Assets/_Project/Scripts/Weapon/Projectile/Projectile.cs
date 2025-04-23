using Assets._Project.Scripts.Core.Interface;
using Assets._Project.Scripts.DroppedDamage;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon.Projectile
{
    public class Projectile : MonoBehaviour
    {
        private Projectile _projectile;
        private Vector2 _direction;
        private WeaponConfig _arrow;
        private Weapons.Weapon _weapon;
        private DroppedDamage.DroppedDamage _droppedDamage;
        private Collider2D _nearestEnemy;

        private void Update()
        {
            GiveBulletAcceleration();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamage damage))
            {
                if (damage is Enemy.Enemy enemy)
                {
                    int randomDamage = Random.Range(_arrow.MinDamage, _arrow.MaxDamage) + _weapon.WeaponData.ExtraDamage;
                    _droppedDamage.SpawnNumber(randomDamage, _nearestEnemy.transform);
                    enemy.Damage(randomDamage);
                    Destroy(_projectile.gameObject);
                }
                Destroy(_projectile.gameObject, 4f);
            }
        }

        public void Initialize(Vector2 direction, Projectile projectile, WeaponConfig arrow, Weapons.Weapon weapon, DroppedDamage.DroppedDamage droppedDamage,
            Collider2D nearestEnemy)
        {
            _direction = direction.normalized;
            _projectile = projectile;
            _arrow = arrow;
            _weapon = weapon;
            _droppedDamage = droppedDamage;
            _nearestEnemy = nearestEnemy;
        }

        private void GiveBulletAcceleration()
        {
            if (_direction != Vector2.zero)
                TranslateBullet();
        }

        private void TranslateBullet() =>
            transform.Translate(_direction * _arrow.AttackSpeed * Time.deltaTime, Space.World);
    }
}