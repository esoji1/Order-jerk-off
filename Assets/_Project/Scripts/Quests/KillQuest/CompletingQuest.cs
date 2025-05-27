using _Project.NPC;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Quests.KillQuest
{
    public class CompletingQuest : MonoBehaviour
    {
        [SerializeField] private NPCWizard _NPCWizard;
        [SerializeField] private Button _clickButton;
        [SerializeField] private GameObject _quest;
        [SerializeField] private GameObject _contentActiveQuest;
        [SerializeField] private GameObject _textCompletedPrefab;

        private bool _isCompleted;
        private GameObject _questClone;
        private GameObject _completedText;

        public bool IsCompleted => _isCompleted;
        public GameObject CompletedText => _completedText;
        public GameObject QuestClone => _questClone;

        private void OnEnable()
        {
            EnemyCounterQuest.Instance.OnAddKill += QuestCompleted;
            _NPCWizard.OnTakeQuest += CloneQuestInActiveQuest;
        }

        private void OnDisable()
        {
            EnemyCounterQuest.Instance.OnAddKill -= QuestCompleted;
            _NPCWizard.OnTakeQuest -= CloneQuestInActiveQuest;
        }

        public void SetCompleted(bool value) => _isCompleted = value;

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

                if (_completedText == null)
                    _completedText = Instantiate(_textCompletedPrefab, _contentActiveQuest.transform);
            }
            Debug.Log(_isCompleted);
        }

        private void CloneQuestInActiveQuest()
        {
            if (_questClone != null)
                Destroy(_questClone);

            _questClone = Instantiate(_quest, _contentActiveQuest.transform);
        }
    }
}
