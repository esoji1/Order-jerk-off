using _Project.Quests.KillQuest;
using System;
using UnityEngine;

namespace _Project.NPC
{
    public class NPCWizard : MonoBehaviour
    {
        [SerializeField] private KillQuestView _killQuestView;

        private KillQuest _killQuest;
        private KillQuest _currentKillQuest;

        public event Action OnTakeQuest;

        public KillQuest CurrentKillQuest => _currentKillQuest;

        private void Awake()
        {
            InitializeQuest();
        }

        private void InitializeQuest()
        {
            CreateKillQuest("Тест", "Описание квеста", 1);
            _killQuestView.UpdateIconEnemyView(_killQuest);
        }

        public void TakeQuest()
        {
            _currentKillQuest = _killQuest;
            OnTakeQuest?.Invoke();
            ChangeQuest();
        }

        public void ChangeQuest()
        {
            _killQuestView.ClearIconEnemyList();

            CreateKillQuest("Тест", "Описание квеста", UnityEngine.Random.Range(1, 4));
            _killQuestView.UpdateIconEnemyView(_killQuest);
        }

        private void CreateKillQuest(string title, string description, int questDifficultly)
        {
            _killQuest = new KillQuest(title, description, questDifficultly);
            string str = "";

            foreach (KillQuestData quest in _killQuest.KillQuestData)
                str += $"{quest.TargetEnemyType}: {quest.RequiredKills}  врагов\n";

            _killQuestView.SetDescription(str);
        }
    }
}
