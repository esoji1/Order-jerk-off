using _Project.SelectionGags;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GivesData", fileName = "GivesData")]
    public class GivesData : ScriptableObject
    {
        [field: SerializeField] public List<Gives> Gives { get; private set; }
    }
}
