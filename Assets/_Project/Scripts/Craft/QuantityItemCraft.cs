using _Project.Inventory.Items;
using System;

namespace _Project.Craft
{
    [Serializable]
    public class QuantityItemCraft
    {
        public BaseItem Item;
        public int Quantity;
    }
}