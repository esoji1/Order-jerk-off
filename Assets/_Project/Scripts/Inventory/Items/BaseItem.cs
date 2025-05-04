using System;
using UnityEngine;

namespace _Project.Inventory.Items
{
    public abstract class BaseItem : MonoBehaviour
    {
        public int Id;
        public string Name;
        public Sprite Sprite;
        public ItemCategory Category;
        public int Price;
        public string Description;

        public abstract Enum GetItemType();
    }
}