using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _Project.Inventory.Items
{
    public class FoodItem : BaseItem
    {
        public TransitionType Type;

        public override Enum GetItemType() => Type;
    }
}
