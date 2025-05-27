using _Project.ScriptableObjects;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Quests.KillQuest
{
    public class KillQuestView : MonoBehaviour
    {
        [SerializeField] private GameObject _windowQuest;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private GameObject _questContent;
        [SerializeField] private IconEnemyData _iconEnemyData;
        [SerializeField] private Button _exit; 

        private List<GameObject> _iconEnemyList = new List<GameObject>();
        private Tween _tween;

        private void OnEnable()
        {
            _exit.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _exit.onClick.RemoveListener(Hide);
        }

        public void Show()
        {
            _windowQuest.SetActive(true);
            _tween = _windowQuest.transform
                .DOScale(1, 0.5f);
        }

        public void Hide()
        {
            _tween.Kill();

            _windowQuest.SetActive(false);
            _windowQuest.transform.localScale = new Vector3(0, 0, 0);
        }

        public void SetDescription(string str) => _description.text = str; 

        public void UpdateIconEnemyView(KillQuest killQuest)
        {
            for (int i = 0; i < _iconEnemyData.ListIconEnemy.Count; i++)
            {
                for (int j = 0; j < killQuest.KillQuestData.Length; j++)
                {
                    KillQuestData quest = killQuest.KillQuestData[j];
                    if (quest.TargetEnemyType.Equals(_iconEnemyData.ListIconEnemy[i].EnemyTypes))
                    {
                        GameObject gameObject = Instantiate(_iconEnemyData.ListIconEnemy[i].Icon, _questContent.transform);
                        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = quest.RequiredKills.ToString();
                        _iconEnemyList.Add(gameObject);
                    }
                }
            }
        }

        public void ClearIconEnemyList()
        {
            for (int i = 0; i < _iconEnemyList.Count; i++)
                Destroy(_iconEnemyList[i]);

            _iconEnemyList.Clear();
        }
    }
}
