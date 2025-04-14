using Assets._Project.Scripts.ResourceExtraction.ScissorsMining;
using Assets._Project.Sctipts.Inventory.Items;
using System;

namespace Assets._Project.Scripts.Inventory.Items
{
    public class GrassItem : BaseItem
    {
        public TypesGrasses TypesGrasses;

        public override Enum GetItemType() => TypesGrasses;
    }
}