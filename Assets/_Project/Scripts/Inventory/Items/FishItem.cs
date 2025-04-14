using Assets._Project.Scripts.ResourceExtraction.FishingRodMining;
using Assets._Project.Sctipts.Inventory.Items;
using System;

namespace Assets._Project.Scripts.Inventory.Items
{
    public class FishItem : BaseItem
    {
        public TypesFish TypesFish;

        public override Enum GetItemType() => TypesFish;
    }
}