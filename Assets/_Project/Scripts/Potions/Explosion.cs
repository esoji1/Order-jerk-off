using _Project.Core.Interface;
using UnityEngine;

namespace _Project.Potions
{
    public class Explosion : MonoBehaviour
    {
        private Player.Player _player;
        private Vector2 _direction;
        private int _damage;
        private int _radiusAttack;
        private ParticleSystem _bom;

        private void Update()
        {
            TranslateBullet();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, _radiusAttack, _player.Config.LayerEnemy);
            bool isFirstSpawm = false;

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out IDamage damage))
                {
                    damage.Damage(_damage);
                    Destroy(gameObject);

                    if (isFirstSpawm == false)
                    {
                        ParticleSystem particleSystem = Instantiate(_bom, transform.position, Quaternion.identity);
                        particleSystem.Play();
                        isFirstSpawm = true;
                        Destroy(particleSystem, 5f);
                    }
                }
            }

            Destroy(gameObject, 5f);
        }

        public void Initialize(Player.Player player, Vector2 direction, int damage, int radiusAttack, ParticleSystem bom)
        {
            _direction = direction;
            _player = player;
            _damage = damage;
            _radiusAttack = radiusAttack;
            _bom = bom;
        }

        private void TranslateBullet() =>
            transform.Translate(_direction * 5f * Time.deltaTime, Space.World);
    }
}