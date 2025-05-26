using _Project.Enemy;
using System;
using Random = UnityEngine.Random;

namespace _Project.Quests.KillQuest
{
    public class KillQuest : Quest
    {
        private const int MinNumberQuest = 1;
        private const int MaxNumberQuest = 4;

        private KillQuestData[] _currentQuest;

        public KillQuestData[] KillQuestData => _currentQuest;

        public KillQuest(string title, string description, int randomQuestDifficulty) : base(title, description)
        {
            int chosenDifficulty = (randomQuestDifficulty > MaxNumberQuest || randomQuestDifficulty < MinNumberQuest) 
                ? Random.Range(MinNumberQuest, MaxNumberQuest) : randomQuestDifficulty;

            _currentQuest = new KillQuestData[randomQuestDifficulty];

            for (int i = 0; i < _currentQuest.Length; i++)
            {
                _currentQuest[i] = new KillQuestData();
                _currentQuest[i].RequiredKills = Random.Range(1, 21);
                _currentQuest[i].TargetEnemyType = (EnemyTypes)Random.Range(0, Enum.GetValues(typeof(EnemyTypes)).Length);
            }
        }
    }
}
