using _Project.Inventory;
using _Project.Inventory.Items;
using _Project.ScriptArtifacts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Artifacts
{
    public class ManagerAtrefact : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private InventoryActive _inventoryActive;

        private ChangeItem _changeItem;

        private Dictionary<TypeArtefact, IArtifact> _artifacts;

        private void Start()
        {
            _artifacts = new Dictionary<TypeArtefact, IArtifact>
            {
                { TypeArtefact.BootsSpeed, new BootsSpeed(_player) },
            };
        }
        public void Initialize(ChangeItem changeItem)
        {
            _changeItem = changeItem;

            _changeItem.OnAddArtefact += AddArtefact;
            _inventoryActive.OnSubstacrtArtefact += SubstractArtefact;
        }

        private void AddArtefact(Cell cell)
        {
            if (cell.Item is ArtefactItem artefact)
            {
                if (_artifacts.TryGetValue(artefact.TypeArtefact, out IArtifact artifact))
                {
                    artifact.Activate();
                }
            }
        }

        private void SubstractArtefact(Cell cell)
        {
            if (cell.Item is ArtefactItem artefact)
            {
                if (_artifacts.TryGetValue(artefact.TypeArtefact, out IArtifact artifact))
                {
                    bool hasOtherArtifacts = _inventoryActive.CellList
                        .Any(c => c.Item != null && c.Item.GetItemType().Equals(cell.Item.GetItemType()) && c.NumberItems > 0);

                    if (hasOtherArtifacts == false)
                    {
                        artifact.Deactivate();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _changeItem.OnAddArtefact -= AddArtefact;
            _inventoryActive.OnSubstacrtArtefact -= SubstractArtefact;
        }
    }
}
