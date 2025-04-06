using Assets._Project.Scripts.Inventory;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Configs/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Item WoodenAxeItem { get; private set; }
        [field: SerializeField] public Item WoodenSwordItem { get; private set; }
    }
}