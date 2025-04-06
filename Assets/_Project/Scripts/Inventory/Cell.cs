using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Inventory
{
    public class Cell : MonoBehaviour
    {
        public Item Item;
        private int _numberItems;
        private bool _IsCellBusy;
        private TextMeshProUGUI _textMeshProUGUI;

        public bool IsCellBusy => _IsCellBusy;
        public int NumberItems => _numberItems;

        private void Awake()
        {
            _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetIsCellBusy(bool value) => _IsCellBusy = value;

        public void AddNumberItems(int value)
        {
            _numberItems += value;

            _textMeshProUGUI.text = _numberItems.ToString();
        }

        public void SubtractNumberItems(int value)
        {
            _numberItems -= value;

            _textMeshProUGUI.text = _numberItems.ToString();
        }
    }
}
