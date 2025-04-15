using Assets._Project.Sctipts.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Configs/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public BaseItem WoodenAxeItem { get; private set; }
        [field: SerializeField] public BaseItem WoodenSwordItem { get; private set; }
        [field: SerializeField] public BaseItem PickItem { get; private set; }
        [field: SerializeField] public BaseItem IronOre {  get; private set; }
        [field: SerializeField] public BaseItem FishingRodItem { get; private set; }
        [field: SerializeField] public BaseItem FishCarpItem { get; private set; }
        [field: SerializeField] public BaseItem FishPerchItem { get; private set; }
        [field: SerializeField] public BaseItem NormalGrassItem { get; private set; }
        [field: SerializeField] public BaseItem ScissorItem { get; private set; }

        [field: SerializeField] public List<BaseItem> Items { get; private set; }
        [field: SerializeField] public List<BaseItem> FishItems { get; private set; }
        [field: SerializeField] public List<BaseItem> OreItems { get; private set; }
        [field: SerializeField] public List<BaseItem> GrassItems { get; private set; }
    }
}