using _Project.Enemy;
using _Project.Enemy.Types;
using System;
using System.Collections.Generic;

namespace _Project.Quests.KillQuest
{
    public class EnemyCounterQuest
    {
        private static EnemyCounterQuest _instance;

        public Dictionary<EnemyType, int> EnemyKillCount = new Dictionary<EnemyType, int>();

        public event Action OnAddKill;

        public static EnemyCounterQuest Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EnemyCounterQuest();
                }
                return _instance;
            }
        }

        public void AddKill(EnemyType type)
        {
            if (EnemyKillCount.ContainsKey(type))
            {
                EnemyKillCount[type]++;
            }
            else
            {
                EnemyKillCount.Add(type, 1);
            }

            OnAddKill?.Invoke();
        }

        public void ClearDictionary() => EnemyKillCount.Clear();
    }
}
