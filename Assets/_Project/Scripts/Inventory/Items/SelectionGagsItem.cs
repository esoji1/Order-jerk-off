using _Project.SelectionGags;
using System;

namespace _Project.Inventory.Items
{
    public class SelectionGagsItem : BaseItem
    {
        public TypesSelectionGags TypesSelectionGags;

        public override Enum GetItemType() => TypesSelectionGags;
    }
}
