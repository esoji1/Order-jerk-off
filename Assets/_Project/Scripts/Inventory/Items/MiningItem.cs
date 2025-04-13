using Assets._Project.Sctipts.Inventory.Items;
using Assets._Project.Sctipts.ResourceExtraction;
using System;

namespace Assets._Project.Scripts.Inventory.Items
{
    public class MiningItem : BaseItem
    {
        public TypesMining TypesMining;

        public override Enum GetItemType() => TypesMining;
    }
}