using _Project.Inventory;
using _Project.Inventory.Items;
using _Project.NPC;
using _Project.ScriptableObjects.Configs;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Quests.KillQuest
{
    public class CompletingQuest : MonoBehaviour
    {
        [SerializeField] private NPCWizard _NPCWizard;
        [SerializeField] private GameObject _activeQuestsWindow;
        [SerializeField] private Button _activeQuestsButton;
        [SerializeField] private Button _clickButton;
        [SerializeField] private Button _exit;
        [SerializeField] private GameObject _quest;
        [SerializeField] private GameObject _contentCloneQuest;
        [SerializeField] private GameObject _textCompletedPrefab;
        [SerializeField] private GameObject _itemSelectionWindow;
        [SerializeField] private Cell _prefabCell;
        [SerializeField] private ItemData _itemData;
        [SerializeField] private GameObject _contentItemSelectionWindow;
        [SerializeField] private Inventory.Inventory _inventory;

        private bool _isCompleted;
        private Tween _tween;
        private GameObject _questClone;
        private GameObject _text;

        public bool IsCompleted => _isCompleted;

        private void OnEnable()
        {
            EnemyCounterQuest.Instance.OnAddKill += QuestCompleted;
            _activeQuestsButton.onClick.AddListener(Show);
            _exit.onClick.AddListener(Hide);
            _clickButton.onClick.AddListener(PickUpQuest);
            _NPCWizard.OnTakeQuest += CloneQuestInActiveQuest;
        }

        private void OnDisable()
        {
            EnemyCounterQuest.Instance.OnAddKill -= QuestCompleted;
            _activeQuestsButton.onClick.RemoveListener(Show);
            _exit.onClick.RemoveListener(Hide);
            _clickButton.onClick.RemoveListener(PickUpQuest);
            _NPCWizard.OnTakeQuest -= CloneQuestInActiveQuest;
        }

        public void Show()
        {
            _activeQuestsWindow.SetActive(true);
            _tween = _activeQuestsWindow.transform
                .DOScale(1, 0.5f);
        }

        public void Hide()
        {
            _tween.Kill();

            _activeQuestsWindow.SetActive(false);
            _activeQuestsWindow.transform.localScale = new Vector3(0, 0, 0);
        }

        private void QuestCompleted()
        {
            if (_NPCWizard.CurrentKillQuest == null)
                return;

            bool allConditionsMet = true;

            for (int i = 0; i < _NPCWizard.CurrentKillQuest.KillQuestData.Length; i++)
            {
                KillQuestData questData = _NPCWizard.CurrentKillQuest.KillQuestData[i];

                if (EnemyCounterQuest.Instance.EnemyKillCount.ContainsKey(questData.TargetEnemyType) == false ||
                    EnemyCounterQuest.Instance.EnemyKillCount[questData.TargetEnemyType] < questData.RequiredKills)
                {
                    allConditionsMet = false;
                    break;
                }
            }

            if (allConditionsMet)
            {
                _isCompleted = true;
                if (_text == null)
                    _text = Instantiate(_textCompletedPrefab, _contentCloneQuest.transform);
            }
            Debug.Log(_isCompleted);
        }

        private void CloneQuestInActiveQuest()
        {
            if (_questClone != null)
                Destroy(_questClone);

            _questClone = Instantiate(_quest, _contentCloneQuest.transform);
        }

        private void PickUpQuest()
        {
            if (_NPCWizard.CurrentKillQuest == null || _isCompleted == false)
                return;

            Destroy(_questClone);
            Destroy(_text);

            Hide();

            _itemSelectionWindow.SetActive(true);
            SpawnItemsSelection();
        }

        private void SpawnItemsSelection()
        {
            List<Cell> list = new List<Cell>();

            for (int i = 0; i < 3; i++)
            {
                Cell cell = Instantiate(_prefabCell, _contentItemSelectionWindow.transform);
                BaseItem rendomBaseItem = _itemData.Items[Random.Range(0, _itemData.Items.Count)];
                BaseItem baseItem = Instantiate(rendomBaseItem, cell.transform);
                RectTransform rectTransform = baseItem.GetComponent<RectTransform>();
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 250);
                cell.Item = baseItem;
                cell.NumberItems = Random.Range(1, 21);
                cell.AddNumberItems(0);
                SetupItemButton(cell, list, rendomBaseItem);
                list.Add(cell);
            }
        }

        private void SetupItemButton(Cell cell, List<Cell> list, BaseItem baseItem)
        {
            if (cell.Item.TryGetComponent(out Button button))
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnItemClicked(cell, list, baseItem));
            }
        }

        private void OnItemClicked(Cell cell, List<Cell> list, BaseItem baseItem)
        {
            for (int i = 0; i < cell.NumberItems; i++)
                _inventory.AddItemInCell(baseItem);

            foreach (Cell cell1 in list)
                Destroy(cell1.gameObject);

            list.Clear();

            _isCompleted = false;
            _itemSelectionWindow.SetActive(false);
        }
    }
}
