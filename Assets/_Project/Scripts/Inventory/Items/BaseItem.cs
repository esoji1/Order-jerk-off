using Assets._Project.Scripts.Inventory;
using System;
using UnityEngine;

namespace Assets._Project.Sctipts.Inventory.Items
{
    public abstract class BaseItem : MonoBehaviour
    {
        public int Id;
        public string Name;
        public Sprite Sprite;
        public ItemCategory Category;
        public int Price;
        public string Description;
        public Enum Type;

        public abstract Enum GetItemType();
    }
}