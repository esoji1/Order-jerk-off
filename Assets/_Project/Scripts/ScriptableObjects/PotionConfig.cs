using _Project.Potions;
using UnityEngine;

namespace _Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PotionConfig", menuName = "ScriptableObjects/Configs/PotionConfig")]
    public class PotionConfig : ScriptableObject
    {
        [field: SerializeField] public BasePotion Prefab { get; private set; }
    }
}