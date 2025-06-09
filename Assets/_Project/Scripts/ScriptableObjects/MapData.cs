using _Project.MapGeneration;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/MapData", fileName = "MapData")]
    public class MapData : ScriptableObject
    {
        [field: SerializeField] public List<Map> Maps {  get; private set; }
    }
}
