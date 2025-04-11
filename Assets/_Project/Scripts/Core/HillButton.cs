using Assets._Project.Scripts.Player;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Core
{
    public class HillButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Player _player;

        private void OnEnable()
        {
            _button.onClick.AddListener(Hill);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Hill);
        }

        private void Hill()
        {
            int currentHealth = _player.Health.HealthValue;
            int maxHealth = _player.PlayerCharacteristics.Health;
            int amountToHeal = Mathf.Clamp(10, 0, maxHealth - currentHealth);

            if (amountToHeal > 0)
            {
                _player.Health.AddHealth(amountToHeal);
                _player.HealthView.AddHealth(amountToHeal);
            }
        }
    }
}