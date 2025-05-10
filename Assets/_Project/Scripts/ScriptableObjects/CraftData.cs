using _Project.Craft;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CraftData", menuName = "ScriptableObjects/Data/CraftData")]
    public class CraftData : ScriptableObject
    {
        [field: SerializeField] public List<Craft> Crafts { get; private set; } 
    }
}