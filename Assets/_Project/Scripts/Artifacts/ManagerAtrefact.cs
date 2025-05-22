using _Project.Inventory;
using Assets._Project.ScriptArtifacts;
using UnityEngine;

namespace _Project.Artifacts
{
    public class ManagerAtrefact : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private InventoryActive _inventoryActive;

        private ChangeItem _changeItem;

        private BootsSpeed _bootsSpeed;

        private void Start()
        {
            _bootsSpeed = new BootsSpeed(_player, _inventoryActive);
        }

        public void Initialize(ChangeItem changeItem)
        {
            _changeItem = changeItem;

            _changeItem.OnAddArtefact += AddArtefact;
            _inventoryActive.OnSubstacrtArtefact += SubstractArtefact;
        }

        private void AddArtefact(Cell cell)
        {
            if(cell.Item.GetItemType().Equals(TypeArtefact.BootsSpeed))
            {
                _bootsSpeed.Use();
            }
            else if (cell.Item.GetItemType().Equals(TypeArtefact.ClawsAttack))
            {

            }
        }

        private void SubstractArtefact(Cell cell)
        {
            if (cell.Item.GetItemType().Equals(TypeArtefact.BootsSpeed))
            {
                _bootsSpeed.SetActiveArtefact(cell);
            }
            else if (cell.Item.GetItemType().Equals(TypeArtefact.ClawsAttack))
            {

            }
        }

        private void OnDestroy()
        {
            _changeItem.OnAddArtefact -= AddArtefact;
            _inventoryActive.OnSubstacrtArtefact -= SubstractArtefact;
        }
    }
}
