using Assets._Project.Scripts.Weapon;
using System;

namespace Assets._Project.Sctipts.Inventory.Items
{
    public class WeaponItem : BaseItem
    {
        public WeaponTypes TypeItem;

        public override Enum GetItemType() => TypeItem;
    }
}