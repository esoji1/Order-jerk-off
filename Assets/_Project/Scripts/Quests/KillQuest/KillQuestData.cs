using _Project.Enemy;
using _Project.Enemy.Types;
using System;

namespace _Project.Quests.KillQuest
{
    [Serializable]
    public class KillQuestData
    {
        public int RequiredKills;
        public EnemyType TargetEnemyType;
    }
}
