using TMPro;
using UnityEngine;

namespace _Project.Player.Pumping
{
    public class CharacteristicsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _attackSpeedText;
        [SerializeField] private TextMeshProUGUI _AddDamageAttackText;

        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;

            _player.OnUp += Show;
        }

        public void Show()
        {
            _healthText.text = $"��������: {_player.PlayerCharacteristics.Health}";
            _attackSpeedText.text = $"�������� �����: {_player.PlayerCharacteristics.AttackSpeed}";
            _AddDamageAttackText.text = $"��� ����: {_player.PlayerCharacteristics.AddDamageAttack}";
        }

        private void OnDestroy()
        {
            _player.OnUp -= Show;
        }
    }
}