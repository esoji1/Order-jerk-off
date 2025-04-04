using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Player.Pumping
{
    public class CoinPlayer : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private TextMeshProUGUI _textCoin;

        private void Start() =>
            _player.Wallet.OnMoneyChange += Show;

        private void OnDestroy() =>
            _player.Wallet.OnMoneyChange -= Show;

        private void Show(int value) =>
            _textCoin.text = $"{value} золота";
    }
}