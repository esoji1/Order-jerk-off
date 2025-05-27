using _Project.NPC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Quests.KillQuest
{
    public class CompletingQuest : MonoBehaviour
    {
        [SerializeField] private NPCWizard _NPCWizard;
        [SerializeField] private GameObject _activeQuestsWindow;
        [SerializeField] private Button _activeQuestsButton;
        [SerializeField] private Button _clickButton;
        [SerializeField] private Button _exit;
        [SerializeField] private GameObject _quest;
        [SerializeField] private GameObject _contentCloneQuest;

        private bool _isCompleted;
        private Tween _tween;
        private GameObject _questClone;

        public bool IsCompleted => _isCompleted;

        private void OnEnable()
        {
            EnemyCounterQuest.Instance.OnAddKill += QuestCompleted;
            _activeQuestsButton.onClick.AddListener(Show);
            _exit.onClick.AddListener(Hide);
            _clickButton.onClick.AddListener(PickUpQuest);
            _NPCWizard.OnTakeQuest += CloneQuestInActiveQuest;
        }

        private void OnDisable()
        {
            EnemyCounterQuest.Instance.OnAddKill -= QuestCompleted; 
            _activeQuestsButton.onClick.RemoveListener(Show);
            _exit.onClick.RemoveListener(Hide);
            _clickButton.onClick.RemoveListener(PickUpQuest);
            _NPCWizard.OnTakeQuest -= CloneQuestInActiveQuest;
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

        private void QuestCompleted()
        {
            if (_NPCWizard.CurrentKillQuest == null)
                return;

            bool allConditionsMet = true;

            for (int i = 0; i < _NPCWizard.CurrentKillQuest.KillQuestData.Length; i++)
            {
                KillQuestData questData = _NPCWizard.CurrentKillQuest.KillQuestData[i];

                if (EnemyCounterQuest.Instance.EnemyKillCount.ContainsKey(questData.TargetEnemyType) == false ||
                    EnemyCounterQuest.Instance.EnemyKillCount[questData.TargetEnemyType] < questData.RequiredKills)
                {
                    allConditionsMet = false;
                    break;
                }
            }

            if (allConditionsMet)
            {
                _isCompleted = true;
            }
            Debug.Log(_isCompleted);
        }

        private void CloneQuestInActiveQuest()
        {
            if (_questClone != null)
                Destroy(_questClone);

            _questClone = Instantiate(_quest, _contentCloneQuest.transform);
        }

        private void PickUpQuest()
        {
            if (_NPCWizard.CurrentKillQuest == null)
                return;

        }
    }
}
