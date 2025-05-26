using _Project.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Quests.KillQuest
{
    public class EnemyCounterQuest : MonoBehaviour   
    {
        private static EnemyCounterQuest _instance;

        public Dictionary<EnemyTypes, int> EnemyKillCount = new Dictionary<EnemyTypes, int>();

        public event Action OnAddKill;

        public static EnemyCounterQuest Instance
        {
            get
            {
                if (_instance == null && Application.isPlaying)
                {
                    _instance = new GameObject("EnemyCounterQuest").AddComponent<EnemyCounterQuest>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        public void AddKill(EnemyTypes type)
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
