using System;
using UnityEngine;

namespace Assets._Project.Scripts.ResourceExtraction.ScissorsMining
{
    public class Grass : MonoBehaviour
    {
        public TypesGrasses TypesGrasses;

        public Enum GetItemType() => TypesGrasses;
    }
}