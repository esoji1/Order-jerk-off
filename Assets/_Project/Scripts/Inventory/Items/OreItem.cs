using _Project.ResourceExtraction.OreMining;
using System;

namespace _Project.Inventory.Items
{
    public class OreItem : BaseItem
    {
        public TypesOre TypesOre;

        public override Enum GetItemType() => TypesOre;
    }
}