using _Project.Inventory.Items;
using _Project.Inventory;
using _Project.NPC;
using _Project.ScriptableObjects.Configs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _Project.SelectionGags;

namespace _Project.Quests.KillQuest
{
    public class ItemSelector : MonoBehaviour
    {
        [SerializeField] private NPCWizard _NPCWizard;
        [SerializeField] private Button _clickButton;
        [SerializeField] private GameObject _itemSelectionWindow;
        [SerializeField] private Cell _prefabCellItemSelection;
        [SerializeField] private ItemData _itemData;
        [SerializeField] private GameObject _contentItemSelectionWindow;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private ActiveQuestView _activeQuestView;
        [SerializeField] private CompletingQuest _completingQuest;
        [SerializeField] private Player.Player _player;

        private void OnEnable() => _clickButton.onClick.AddListener(PickUpQuest);

        private void OnDisable() => _clickButton.onClick.RemoveListener(PickUpQuest);

        private void PickUpQuest()
        {
            if (_NPCWizard.CurrentKillQuest == null || _completingQuest.IsCompleted == false)
                return;

            Destroy(_completingQuest.QuestClone);
            Destroy(_completingQuest.CompletedText);

            _activeQuestView.Hide();

            _itemSelectionWindow.SetActive(true);
            SpawnItemsSelection();
        }

        private void SpawnItemsSelection()
        {
            List<Cell> list = new List<Cell>();

            for (int i = 0; i < 3; i++)
                SpawnGameObject(list);
        }

        private void SpawnGameObject(List<Cell> list)
        {
            Cell cell = Instantiate(_prefabCellItemSelection, _contentItemSelectionWindow.transform);
            BaseItem rendomBaseItem = _itemData.Items[Random.Range(0, _itemData.Items.Count)];
            BaseItem baseItem = Instantiate(rendomBaseItem, cell.transform);
            ChangeImageSize(baseItem);
            InitializeDataCell(cell, baseItem, rendomBaseItem);
            SetupItemButton(cell, list, rendomBaseItem); 
            list.Add(cell);
        }

        private void ChangeImageSize(BaseItem baseItem)
        {
            RectTransform rectTransform = baseItem.GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 250);
        }

        private void InitializeDataCell(Cell cell, BaseItem baseItem,BaseItem rendomBaseItem)
        {
            cell.Item = baseItem;
            cell.NumberItems = Random.Range(1, 21);
            cell.AddNumberItems(0);
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
            if (cell.Item.Category.Equals(ItemCategory.SelectionGags))
            {
                SelectionGagsItem selectionGagsItem = cell.Item as SelectionGagsItem;
                
                if(selectionGagsItem.TypesSelectionGags.Equals(TypesSelectionGags.Experience))
                    _player.AddExperience(cell.NumberItems);
                else if (selectionGagsItem.TypesSelectionGags.Equals(TypesSelectionGags.Coin))
                    _player.AddMoney(cell.NumberItems);

                ClearAction(list);
                return;
            }

            for (int i = 0; i < cell.NumberItems; i++)
                _inventory.AddItemInCell(baseItem);

            ClearAction(list);
        }

        private void ClearAction(List<Cell> list)
        {
            foreach (Cell cell1 in list)
                Destroy(cell1.gameObject);

            list.Clear();

            _completingQuest.SetCompleted(false);
            _itemSelectionWindow.SetActive(false);
        }
    }
}
