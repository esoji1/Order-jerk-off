using _Project.ResourceExtraction.FishingRodMining;
using System;

namespace _Project.Inventory.Items
{
    public class FishItem : BaseItem
    {
        public TypesFish TypesFish;

        public override Enum GetItemType() => TypesFish;
    }
}