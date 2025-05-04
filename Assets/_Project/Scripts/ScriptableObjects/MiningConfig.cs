using _Project.ResourceExtraction;
using UnityEngine;

namespace _Project.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "MiningConfig", menuName = "ScriptableObjects/Configs/MiningConfig")]
    public class MiningConfig : ScriptableObject
    {
        [field: SerializeField] public BaseMining Prefab { get; private set; }
        [field: SerializeField] public float MiningSpeed { get; private set; }
        [field: SerializeField] public float ExtractionTime { get; private set; }
    }
}