using _Project.Artifacts;
using System;

namespace _Project.Inventory.Items
{
    public class ArtefactItem : BaseItem
    {
        public TypeArtefact TypeArtefact;

        public override Enum GetItemType() => TypeArtefact;
    }
}
