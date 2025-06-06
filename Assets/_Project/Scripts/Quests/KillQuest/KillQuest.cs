using _Project.Enemy;
using System;
using System.Linq;
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

            EnemyType[] allEnemyTypes = (EnemyType[])Enum.GetValues(typeof(EnemyType));

            int maxPossibleQuests = Math.Min(chosenDifficulty, allEnemyTypes.Length);
            _currentQuest = new KillQuestData[maxPossibleQuests];

            EnemyType[] shuffledEnemyTypes = allEnemyTypes.OrderBy(x => Random.value).ToArray();

            for (int i = 0; i < _currentQuest.Length; i++)
            {
                _currentQuest[i] = new KillQuestData
                {
                    RequiredKills = Random.Range(1, 21),
                    TargetEnemyType = shuffledEnemyTypes[i]
                };
            }
        }
    }
}
