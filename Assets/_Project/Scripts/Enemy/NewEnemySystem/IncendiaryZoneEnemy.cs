using _Project.Player;
using Assets._Project.Scripts.Core;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _Project.Enemy
{
    public class IncendiaryZoneEnemy : MonoBehaviour
    {
        private float _damage;
        private float _radiusAttack;

        private ParticleSystem _particleSystem;

        private Coroutine _coroutine;
        private float _delay;

        public void Initialize(int damage, float radiusAttack)
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();

            _damage = damage;
            _radiusAttack = radiusAttack;
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

                Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, _radiusAttack, Layers.LayerPlayer);

                foreach (Collider2D collider in collider2D)
                {
                    if (collider.TryGetComponent(out Player.Player player))
                    {
                        player.Damage((int)_damage);
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
