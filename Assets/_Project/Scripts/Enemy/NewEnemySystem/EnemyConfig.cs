using System.Collections.Generic;
using UnityEngine;

namespace _Project.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public Enemy Prefab;
        [field: SerializeField] public int Health;
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float AttackRadius;
        [field: SerializeField] public float VisibilityRadius;
        [field: SerializeField] public int AmountExperienceDropped;
        [field: SerializeField] public int AmountGoldDropped;
        [field: SerializeField] public int Damage;
        [field: SerializeField] public List<MovementBreakReasonType> MovementBreakReason;
    }
}
