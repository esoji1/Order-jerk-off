using System.Collections;
using TMPro;
using UnityEngine;

namespace _Project.Core.Projectile
{
    public class IncendiaryZone : MonoBehaviour
    {
        private float _damage;
        private float _radiusAttack;
        private Player.Player _player;

        private ParticleSystem _particleSystem;

        private DroppedDamage.DroppedDamage _droppedDamage;

        private Coroutine _coroutine;
        private float _delay;

        public void Initialize(int damage, int radiusAttack, Player.Player player, TextMeshProUGUI textDamage, Canvas dynamic)
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();

            _damage = damage;
            _radiusAttack = radiusAttack;
            _player = player;
            _delay = 1f;

            ParticleSystem.ShapeModule shapeModule = _particleSystem.shape;
            shapeModule.radius = radiusAttack;
            _particleSystem.Play();

            _droppedDamage = new DroppedDamage.DroppedDamage(textDamage, dynamic);

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
                    if (collider.TryGetComponent(out Enemy.Behaviors.Enemy enemy))
                    {
                        enemy.Damage((int)_damage);
                        _droppedDamage.SpawnNumber((int)_damage, enemy.transform);
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
