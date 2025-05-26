using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Quests.KillQuest
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private GameObject _windowQuest;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Button _exit;

        private Tween _tween;

        private void OnEnable()
        {
            _exit.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _exit.onClick.RemoveListener(Hide);
        }

        public void Show()
        {
            _windowQuest.SetActive(true);
            _tween = _windowQuest.transform
                .DOScale(1, 0.5f);
        }

        public void Hide()
        {
            _tween.Kill();

            _windowQuest.SetActive(false);
            _windowQuest.transform.localScale = new Vector3(0, 0, 0);
        }

        public void SetDescription(string str)
        {
            _description.text = str;
        }
    }
}
