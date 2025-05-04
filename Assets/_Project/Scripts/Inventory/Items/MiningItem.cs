using _Project.ResourceExtraction;
using System;

namespace _Project.Inventory.Items
{
    public class MiningItem : BaseItem
    {
        public TypesMining TypesMining;

        public override Enum GetItemType() => TypesMining;
    }
}