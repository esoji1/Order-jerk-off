using _Project.Enemy;
using System;

namespace _Project.Quests.KillQuest
{
    [Serializable]
    public class KillQuestData
    {
        public int RequiredKills;
        public EnemyTypes TargetEnemyType;
    }
}
