using System;
using UnityEngine;

namespace Assets._Project.Scripts.ResourceExtraction.OreMining
{
    public abstract class Ore : MonoBehaviour
    {
        public abstract Enum GetItemType();
    }
}
