using _Project.Inventory.Items;
using TMPro;
using UnityEngine;

namespace _Project.Inventory
{
    public class Cell : MonoBehaviour
    {
        public BaseItem Item;
        public int NumberItems;
        private bool _IsCellBusy;
        private TextMeshProUGUI _textMeshProUGUI;

        public bool IsCellBusy => _IsCellBusy;

        private void Awake()
        {
            _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetIsCellBusy(bool value) => _IsCellBusy = value;

        public void AddNumberItems(int value)
        {
            NumberItems += value;
            UpdateText();
        }

        public void SubtractNumberItems(int value)
        {
            NumberItems -= value;
            UpdateText();
        }

        private void UpdateText()
        {
            string result = NumberItems > 0 ? NumberItems.ToString() : "";
            _textMeshProUGUI.text = result;
        }
    }
}
