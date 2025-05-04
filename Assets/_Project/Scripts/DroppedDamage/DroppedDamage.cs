using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.DroppedDamage
{
    public class DroppedDamage
    {
        private TextMeshProUGUI _textDamage;
        private Canvas _canvas;

        public DroppedDamage(TextMeshProUGUI textDamage, Canvas canvas)
        {
            _textDamage = textDamage;
            _canvas = canvas;
        }

        public void SpawnNumber(int damage, Transform transform)
        {
            TextMeshProUGUI textDamage = Object.Instantiate(_textDamage, _canvas.transform);
            textDamage.transform.position = transform.position;
            textDamage.text = damage.ToString();
            Sequence _animation = DOTween.Sequence();

            _animation
                .Append(textDamage.transform.DOMoveY(textDamage.transform.position.y + 1f, 2f))
                .Join(textDamage.DOFade(0, 2f))
                .OnComplete(() => Object.Destroy(textDamage.gameObject));
        }
    }
}