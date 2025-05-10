using _Project.Inventory.Items;
using System;
using System.Collections.Generic;

namespace _Project.Craft
{
    [Serializable]
    public class Craft 
    {
        public BaseItem CraftItem;
        public List<QuantityItemCraft> List;
    }
}