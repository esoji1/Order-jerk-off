using _Project.ResourceExtraction.ScissorsMining;
using System;

namespace _Project.Inventory.Items
{
    public class GrassItem : BaseItem
    {
        public TypesGrasses TypesGrasses;

        public override Enum GetItemType() => TypesGrasses;
    }
}