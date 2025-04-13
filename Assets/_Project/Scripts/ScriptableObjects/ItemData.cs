using Assets._Project.Sctipts.Inventory.Items;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Configs/ItemData")]
    public class ItemData : ScriptableObject
    {
        [Header("������")]
        [field: SerializeField] public BaseItem WoodenAxeItem { get; private set; }
        [field: SerializeField] public BaseItem WoodenSwordItem { get; private set; }
        [Header("�������� ��� ������")]
        [field: SerializeField] public BaseItem PickItem { get; private set; }
        [Header("����")]
        [field: SerializeField] public BaseItem IronOre {  get; private set; }
    }
}