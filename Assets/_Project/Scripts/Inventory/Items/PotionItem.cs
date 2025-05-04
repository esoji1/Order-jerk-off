using _Project.Potions;
using System;

namespace _Project.Inventory.Items
{
    public class PotionItem : BaseItem
    {
        public TypesPotion ItemType;

        public override Enum GetItemType() => ItemType;
    }
}