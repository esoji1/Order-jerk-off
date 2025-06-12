using TMPro;
using UnityEngine;

namespace _Project.MapGeneration.Food
{
    public class FoodView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textFood;
        [SerializeField] private int _startAmauntFood;

        public int AmauntFood => int.Parse(_textFood.text);

        private void Awake() => _textFood.text = _startAmauntFood.ToString();

        public void SubtractFood(int value)
        {
            _textFood.text = (int.Parse(_textFood.text) - value).ToString();
        }

        public void AddFood(int value)
        {
            _textFood.text = (int.Parse(_textFood.text) + value).ToString();
        }

        public void SetFood(int value)
        {
            _textFood.text = value.ToString();  
        }
    }
}