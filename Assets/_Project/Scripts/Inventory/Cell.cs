using _Project.Inventory.Items;
using TMPro;
using UnityEngine;

namespace _Project.Inventory
{
    public class Cell : MonoBehaviour
    {
        public BaseItem Item;
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
            UpdateText();
        }

        public void SubtractNumberItems(int value)
        {
            _numberItems -= value;
            UpdateText();
        }

        private void UpdateText()
        {
            string result = _numberItems > 0 ? _numberItems.ToString() : "";
            _textMeshProUGUI.text = result;
        }
    }
}
