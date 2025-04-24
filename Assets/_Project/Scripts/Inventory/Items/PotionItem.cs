using Assets._Project.Sctipts.Inventory.Items;
using System;

namespace Assets._Project.Scripts.Inventory.Items
{
    public class PotionItem : BaseItem
    {
        public TypesPotion ItemType;

        public override Enum GetItemType() => ItemType;
    }
}