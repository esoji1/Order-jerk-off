using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.ImprovingCharacteristicsPlayer
{
    public class ChoosingUpgrade : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private GameObject _windowUpgrade;
        [SerializeField] private Button _first;
        [SerializeField] private Button _two;
        [SerializeField] private Button _three;

        private Tween _tween;

        public event Action OnUp;

        private void OnEnable()
        {
            _player.OnWindowUpgrade += Show;

            _first.onClick.AddListener(UpgradeHealth);
            _two.onClick.AddListener(UpgradeSpeedAttack);
            _three.onClick.AddListener(UpgradeAddDamage);
        }

        private void OnDisable()
        {
            _player.OnWindowUpgrade -= Show;

            _first.onClick.RemoveListener(UpgradeHealth);
            _two.onClick.RemoveListener(UpgradeSpeedAttack);
            _three.onClick.RemoveListener(UpgradeAddDamage);
        }

        private void Show()
        {
            _windowUpgrade.SetActive(true);
            _tween = _windowUpgrade.transform
                .DOScale(1, 0.5f);
        }

        private void Hide()
        {
            _tween.Kill();
            _windowUpgrade.SetActive(false);
            _windowUpgrade.transform.localScale = new Vector3(0, 0, 0);
        }

        private void UpgradeHealth()
        {
            _player.PlayerCharacteristics.Health += 20;
            _player.Health.SetHealth(_player.PlayerCharacteristics.Health);
            OnUp?.Invoke();
            Hide();
        }

        private void UpgradeSpeedAttack()
        {
            _player.PlayerCharacteristics.AttackSpeed += 1;
            _player.PlayerCharacteristics.ReturnInitialAttackPosition += 0.02f;
            OnUp?.Invoke();
            Hide();
        }

        private void UpgradeAddDamage()
        {
            _player.PlayerCharacteristics.AddDamageAttack += 2;
            OnUp?.Invoke();
            Hide();
        }
    }
}