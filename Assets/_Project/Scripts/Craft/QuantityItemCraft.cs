using Assets._Project.Sctipts.Inventory.Items;
using System;

namespace Assets._Project.Scripts.Craft
{
    [Serializable]
    public class QuantityItemCraft
    {
        public BaseItem Item;
        public int Quantity;
    }
}