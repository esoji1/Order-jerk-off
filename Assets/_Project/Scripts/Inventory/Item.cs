using Assets._Project.Scripts.Weapon;
using UnityEngine;

namespace Assets._Project.Scripts.Inventory
{
    public class Item : MonoBehaviour
    {
        public int Id;
        public string Name;
        public Sprite Sprite;
        public ItemCategory Category;
        public int Price;

        [Header("Weapon Settings")]
        public WeaponTypes WeaponType;

        [Header("Resource Settings")]
        public ResourceType ResourceType;
    }
}
