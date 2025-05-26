using _Project.Quests.KillQuest;
using UnityEngine;

namespace _Project.NPC
{
    public class NPCWizard : MonoBehaviour
    {
        [SerializeField] private QuestView _questView;

        private KillQuest _killQuest;
        private KillQuest _currentKillQuest;

        public KillQuest CurrentKillQuest => _currentKillQuest; 

        private void Awake()
        {
            InitializeQuest();
        }

        private void InitializeQuest()
        {
            _killQuest = new KillQuest("Тест", "Описание квеста", 1);

            string str = "";

            foreach (KillQuestData quest in _killQuest.KillQuestData)
            {
                str += $"{quest.TargetEnemyType}: {quest.RequiredKills}  врагов\n";
            }

            _questView.SetDescription(str);
        }

        public void TakeQuest()
        {
            _currentKillQuest = _killQuest;
        }

        public void ChangeQuest()
        {
            _killQuest = new KillQuest("Тест", "Описание квеста", Random.Range(1, 4));

            string str = "";

            foreach (KillQuestData quest in _killQuest.KillQuestData)
            {
                str += $"{quest.TargetEnemyType}: {quest.RequiredKills}  врагов\n";
            }

            _questView.SetDescription(str);
        }
    }
}
