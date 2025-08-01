using UnityEngine;

namespace _Project.Core.HealthSystem
{
    public class HealthView : MonoBehaviour
    {
        private HealthInfo _healthInfo;
        private IOnDamage _onDamage;
        private Player.Player _player;

        private int _currentHp;
        private int _maxHp;

        public void Initialize(IOnDamage onDamage, int maxHealth, HealthInfo healthInfo, Player.Player player)
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
                _player.ChoosingUpgrade.OnUp += UpdateParameters;
        }

        private void OnDestroy()
        {
            if (_player != null)
                _player.ChoosingUpgrade.OnUp -= UpdateParameters;

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

        public void ResubscribeEvents(IOnDamage onDamage)
        {
            _onDamage = onDamage;
            _onDamage.OnDamage += Damage;
        }

        public void UpdateParameters()
        {
            _maxHp = _player.PlayerCharacteristics.Health;
            _healthInfo.InstantiatedHealthBar.maxValue = _maxHp;
            _currentHp = _player.PlayerCharacteristics.Health;
            UpdateHealthBar();
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

            if (_healthInfo.TextHp != null)
                _healthInfo.TextHp.text = $"{_currentHp}/{_maxHp}";
        }
    }
}