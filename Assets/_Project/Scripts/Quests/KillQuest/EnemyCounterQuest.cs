using _Project.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Quests.KillQuest
{
    public class EnemyCounterQuest : MonoBehaviour   
    {
        private static EnemyCounterQuest _instance;

        private Dictionary<EnemyTypes, int> enemyKillCount = new Dictionary<EnemyTypes, int>();

        public static EnemyCounterQuest Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("EnemyCounterQuest").AddComponent<EnemyCounterQuest>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        public void AddKill(EnemyTypes type)
        {
            if (enemyKillCount.ContainsKey(type))
            {
                enemyKillCount[type]++;
            }
            else
            {
                enemyKillCount.Add(type, 1);
            }

            Debug.Log($"Убито {type}: {enemyKillCount[type]}");
        }

        public void ClearDictionary() => enemyKillCount.Clear();
    }
}
