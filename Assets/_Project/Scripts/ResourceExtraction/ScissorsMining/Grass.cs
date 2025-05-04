using System;
using UnityEngine;

namespace _Project.ResourceExtraction.ScissorsMining
{
    public class Grass : MonoBehaviour
    {
        public TypesGrasses TypesGrasses;

        public Enum GetItemType() => TypesGrasses;
    }
}