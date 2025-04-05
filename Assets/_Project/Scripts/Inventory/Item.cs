using UnityEngine;

namespace Assets._Project.Scripts.Inventory
{
    public class Item : MonoBehaviour
    {
        public int Id;
        public string Name;
        public Sprite Sprite;
        public ItemCategory Category;

        [Header("Weapon Settings")]
        public WeaponType WeaponType;

        [Header("Resource Settings")]
        public ResourceType ResourceType;
    }
}
