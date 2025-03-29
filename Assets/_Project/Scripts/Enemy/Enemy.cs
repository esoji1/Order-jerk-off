using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Core.Interface;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamage
    {
        private Health _health;
        private EnemyConfig _config;

        public virtual void Initialize(EnemyConfig config)
        {
            _config = config;
            _health = new Health(_config.Health);

            _health.OnDie += Die;
        }

        public void Damage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void Die()
        {
            _health.OnDie -= Die;

            Destroy(gameObject);
        }
    }
}