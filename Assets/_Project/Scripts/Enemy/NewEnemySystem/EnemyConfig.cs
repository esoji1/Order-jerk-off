using System.Collections.Generic;
using UnityEngine;

namespace _Project.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public Enemy Prefab;
        [field: SerializeField] public int Health;
        [field: SerializeField] public int AmountExperienceDropped;
        [field: SerializeField] public int AmountGoldDropped;
        [field: SerializeField] public List<MovementBreakReasonType> MovementBreakReason;
    }
}
