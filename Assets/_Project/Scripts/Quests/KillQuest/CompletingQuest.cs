using _Project.NPC;
using UnityEngine;

namespace _Project.Quests.KillQuest
{
    public class CompletingQuest : MonoBehaviour
    {
        [SerializeField] private NPCWizard _NPCWizard;

        private bool _isCompleted;

        public bool IsCompleted => _isCompleted;

        private void OnEnable()
        {
            EnemyCounterQuest.Instance.OnAddKill += QuestCompleted;
        }

        private void OnDisable()
        {
            EnemyCounterQuest.Instance.OnAddKill -= QuestCompleted;
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
    }
}
