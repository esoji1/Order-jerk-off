using _Project.Enemy;
using _Project.Enemy.Types;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public Enemy.Behaviors.Enemy Prefab;
        [field: SerializeField] public int Health;
        [field: SerializeField] public int AmountExperienceDropped;
        [field: SerializeField] public int AmountGoldDropped;
        [field: SerializeField] public List<MovementBreakReasonType> MovementBreakReason;
    }
}
