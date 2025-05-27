using _Project.NPC;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Quests.KillQuest
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField] private NPCWizard _NPCWizard;
        [SerializeField] private Button _changeQuest;
        [SerializeField] private Button _takeQuest;
        [SerializeField] private CompletingQuest _completingQuest;

        private void OnEnable()
        {
            _changeQuest.onClick.AddListener(ChangeQuest);
            _takeQuest.onClick.AddListener(TakeQuest);
        }

        private void OnDisable()
        {
            _changeQuest.onClick.RemoveListener(ChangeQuest);
            _takeQuest.onClick.RemoveListener(TakeQuest);
        }

        private void ChangeQuest()
        {
            _NPCWizard.ChangeQuest();
        }

        private void TakeQuest()
        {
            if (_completingQuest.IsCompleted)
                return;

            EnemyCounterQuest.Instance.ClearDictionary();
            _NPCWizard.TakeQuest();
        }
    }
}
