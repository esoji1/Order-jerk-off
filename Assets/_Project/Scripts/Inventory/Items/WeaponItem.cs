using _Project.Weapon;
using System;

namespace _Project.Inventory.Items
{
    public class WeaponItem : BaseItem
    {
        public WeaponTypes TypeItem;

        private Improvements.ImprovementWeaponData _improvementWeaponData;

        private int _level;

        public int Level => _level;
        public Improvements.ImprovementWeaponData ImprovementWeaponData => _improvementWeaponData;

        private void Start()
        {
            _level = 1;
            _improvementWeaponData = new Improvements.ImprovementWeaponData();
        }

        public override Enum GetItemType() => TypeItem;

        public void SetLevel(int value) => _level = value;
    }
}