using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Craft
{
    public class ViewBookCrafts : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _window;
        [SerializeField] private Button _exitWindow;

        private Tween _tween;

        private void OnEnable()
        {
            _button.onClick.AddListener(Show);
            _exitWindow.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Show);
            _exitWindow.onClick.RemoveListener(Hide);
        }

        private void Show()
        {
            _window.SetActive(true);
            _tween = _window.transform
                .DOScale(1, 0.5f);
        }

        private void Hide()
        {
            _tween?.Kill();
            _window.SetActive(false);
            _window.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}