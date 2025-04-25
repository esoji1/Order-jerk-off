using Assets._Project.Scripts.Craft;
using Assets._Project.Sctipts.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Sctipts.Craft
{
    public class Craft : MonoBehaviour
    {
        [SerializeField] private BaseItem _craftItem;
        [SerializeField] private List<QuantityItemCraft> _list;

        public BaseItem CraftItem => _craftItem;
        public List<QuantityItemCraft> List => _list;
    }
}