using _Project.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Craft
{
    public class Craft : MonoBehaviour
    {
        [SerializeField] private BaseItem _craftItem;
        [SerializeField] private List<QuantityItemCraft> _list;

        public BaseItem CraftItem => _craftItem;
        public List<QuantityItemCraft> List => _list;
    }
}