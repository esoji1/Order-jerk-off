using System.Collections;
using UnityEngine;

namespace _Project.Potions
{
    public class IncendiaryZone : MonoBehaviour
    {
        private float _damage;
        private float _radiusAttack;
        private Player.Player _player;

        private ParticleSystem _particleSystem;

        private Coroutine _coroutine;
        private float _delay;

        public void Initialize(int damage, int radiusAttack, Player.Player player)
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();

            _damage = damage;
            _radiusAttack = radiusAttack;
            _player = player;
            _delay = 1f;

            ParticleSystem.ShapeModule shapeModule = _particleSystem.shape;
            shapeModule.radius = radiusAttack;
            _particleSystem.Play();

            _coroutine = StartCoroutine(Attack());
            Destroy(gameObject, 5f);
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);

                Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, _radiusAttack, _player.Config.LayerEnemy);

                foreach (Collider2D collider in collider2D)
                {
                    if (collider.TryGetComponent(out Enemy.Enemys.Enemy enemy))
                    {
                        enemy.Damage((int)_damage);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
