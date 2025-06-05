using System.Collections;
using UnityEngine;

namespace _Project.Weapon.Effect
{
    public class PoisonEffect : MonoBehaviour
    {
        private int _damagePerTick = 10;
        private float _tickInterval = 1f;
        private float _duration = 5f;

        private Enemy.Enemy _enemy;
        private Coroutine _damageCoroutine;

        private void Awake()
        {
            _enemy = GetComponent<Enemy.Enemy>();
            _damageCoroutine = StartCoroutine(DealDamageOverTime());
        }

        private IEnumerator DealDamageOverTime()
        {
            float endTime = Time.time + _duration;

            while (Time.time < endTime)
            {
                _enemy.Damage(_damagePerTick);
                yield return new WaitForSeconds(_tickInterval);
            }

            Destroy(this);
        }

        private void OnDestroy()
        {
            if (_damageCoroutine != null)
                StopCoroutine(_damageCoroutine);
        }

        public void RefreshDuration()
        {
            if (_damageCoroutine != null)
                StopCoroutine(_damageCoroutine);

            _damageCoroutine = StartCoroutine(DealDamageOverTime());
        }
    }
}
