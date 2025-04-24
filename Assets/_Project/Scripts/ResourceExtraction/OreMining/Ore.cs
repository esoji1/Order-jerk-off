using Assets._Project.Sctipts.ResourceExtraction.OreMining;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.ResourceExtraction.OreMining
{
    public class Ore : MonoBehaviour
    {
        public TypesOre TypesOre;

        public Enum GetItemType() => TypesOre;
    }
}
