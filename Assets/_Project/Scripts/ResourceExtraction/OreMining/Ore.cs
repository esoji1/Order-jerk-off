using System;
using UnityEngine;

namespace _Project.ResourceExtraction.OreMining
{
    public class Ore : MonoBehaviour
    {
        public TypesOre TypesOre;

        public Enum GetItemType() => TypesOre;
    }
}
