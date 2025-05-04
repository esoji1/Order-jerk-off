using _Project.Weapon;
using System;

namespace _Project.Inventory.Items
{
    public class WeaponItem : BaseItem
    {
        public WeaponTypes TypeItem;

        public override Enum GetItemType() => TypeItem;
    }
}