using Assets._Project.Sctipts.Inventory.Items;
using Assets._Project.Sctipts.ResourceExtraction.OreMining;
using System;

namespace Assets._Project.Scripts.Inventory.Items
{
    public class OreItem : BaseItem
    {
        public TypesOre TypesOre;

        public override Enum GetItemType() => TypesOre;
    }
}