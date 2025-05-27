using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Quests.KillQuest
{
    public class ActiveQuestView : MonoBehaviour
    {
        [SerializeField] private GameObject _activeQuestsWindow;
        [SerializeField] private Button _activeQuestsButton;
        [SerializeField] private Button _exit;

        private Tween _tween;

        private void OnEnable()
        {
            _activeQuestsButton.onClick.AddListener(Show);
            _exit.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _activeQuestsButton.onClick.RemoveListener(Show);
            _exit.onClick.RemoveListener(Hide);
        }

        public void Show()
        {
            _activeQuestsWindow.SetActive(true);
            _tween = _activeQuestsWindow.transform
                .DOScale(1, 0.5f);
        }

        public void Hide()
        {
            _tween.Kill();

            _activeQuestsWindow.SetActive(false);
            _activeQuestsWindow.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
