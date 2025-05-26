using _Project.Quests.KillQuest;
using _Project.ScriptableObjects;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Project.NPC
{
    public class NPCWizard : MonoBehaviour
    {
        [SerializeField] private QuestView _questView;
        [SerializeField] private GameObject _questContent;
        [SerializeField] private IconEnemyData _iconEnemyData;

        private KillQuest _killQuest;
        private KillQuest _currentKillQuest;
        private List<GameObject> _iconEnemyList = new List<GameObject>();

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
            UpdateIconEnemyView();
        }

        public void TakeQuest()
        {
            _currentKillQuest = _killQuest;
        }

        public void ChangeQuest()
        {
            for (int i = 0; i < _iconEnemyList.Count; i++)
            {
                Destroy(_iconEnemyList[i]);
            }

            _iconEnemyList.Clear();

            _killQuest = new KillQuest("Тест", "Описание квеста", Random.Range(1, 4));

            string str = "";

            foreach (KillQuestData quest in _killQuest.KillQuestData)
            {
                str += $"{quest.TargetEnemyType}: {quest.RequiredKills}  врагов\n";
            }

            _questView.SetDescription(str);
            UpdateIconEnemyView();
        }

        private void UpdateIconEnemyView()
        {
            for (int i = 0; i < _iconEnemyData.ListIconEnemy.Count; i++)
            {
                for (int j = 0; j < _killQuest.KillQuestData.Length; j++)
                {
                    KillQuestData quest = _killQuest.KillQuestData[j];
                    if (quest.TargetEnemyType.Equals(_iconEnemyData.ListIconEnemy[i].EnemyTypes))
                    {
                        GameObject gameObject = Instantiate(_iconEnemyData.ListIconEnemy[i].Icon, _questContent.transform);
                        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = quest.RequiredKills.ToString();
                        _iconEnemyList.Add(gameObject);
                    }
                }
            }
        }
    }
}
