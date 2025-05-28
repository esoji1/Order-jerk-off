using UnityEngine;

namespace _Project.Potions
{
    public class Molotov : MonoBehaviour
    {
        private Player.Player _player;
        private Vector2 _direction;
        private int _damage;
        private int _radiusAttack;
        private IncendiaryZone _incendiaryZonePrefab;

        private void Update()
        {
            TranslateBullet();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy.Enemys.Enemy _))
            {
                Instantiate(_incendiaryZonePrefab, transform.position, Quaternion.identity, null).Initialize(_damage, _radiusAttack, _player);
                Destroy(gameObject);
            }
        }

        public void Initialize(Player.Player player, Vector2 direction, int damage, int radiusAttack, IncendiaryZone incendiaryZonePrefab)
        {
            _direction = direction;
            _player = player;
            _damage = damage;
            _radiusAttack = radiusAttack;
            _incendiaryZonePrefab = incendiaryZonePrefab;
        }

        private void TranslateBullet() =>
            transform.Translate(_direction * 5f * Time.deltaTime, Space.World);
    }
}
