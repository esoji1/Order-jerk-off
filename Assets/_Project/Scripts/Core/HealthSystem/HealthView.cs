using Assets._Project.Scripts.Player;
using UnityEngine;

namespace Assets._Project.Sctipts.Core.HealthSystem
{
    public class HealthView : MonoBehaviour
    {
        private HealthInfo _healthInfo;
        private IOnDamage _onDamage;
        private Player _player;

        private int _currentHp;
        private int _maxHp;

        public void Initialize(IOnDamage onDamage, int maxHealth, HealthInfo healthInfo, Player player)
        {
            _maxHp = maxHealth;
            _currentHp = maxHealth;
            _onDamage = onDamage;
            _healthInfo = healthInfo;
            if (player != null)
                _player = player;

            _healthInfo.InstantiatedHealthBar.maxValue = _maxHp;
            UpdateHealthBar();

            _onDamage.OnDamage += Damage;
            if (player != null)
                _player.OnUp += UpdateParameters;
        }

        private void OnDestroy()
        {
            if (_player != null)
                _player.OnUp -= UpdateParameters;

            _onDamage.OnDamage -= Damage;
        }

        public void FollowTargetHealth() =>
            _healthInfo.SetPositon(_onDamage.PointHealth.transform);

        public void AddHealth(int value)
        {
            _currentHp += value;

            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);

            UpdateHealthBar();

            if (_currentHp <= 0)
                _onDamage.OnDamage -= Damage;
        }

        private void Damage(int damage)
        {
            _currentHp -= damage;

            _currentHp = Mathf.Clamp(_currentHp, 0, _maxHp);

            UpdateHealthBar();

            if (_currentHp <= 0)
                _onDamage.OnDamage -= Damage;
        }


        private void UpdateHealthBar()
        {
            _healthInfo.InstantiatedHealthBar.value = _currentHp;
        }

        private void UpdateParameters()
        {
            _maxHp = _player.PlayerCharacteristics.Health;
            _healthInfo.InstantiatedHealthBar.maxValue = _maxHp;
            _currentHp = _player.PlayerCharacteristics.Health;
            UpdateHealthBar();
        }
    }
}