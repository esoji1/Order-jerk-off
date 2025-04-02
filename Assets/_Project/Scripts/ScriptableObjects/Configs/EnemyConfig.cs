using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public Enemy.Enemy Prefab;
        [field: SerializeField] public int Health;
        [field: SerializeField] public int Damage;
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float AttackRadius;
        [field: SerializeField] public float VisibilityRadius;
        [field: SerializeField] public int ReturnInitialAttackPosition;
        [field: SerializeField] public int AmountExperienceDropped;
        [field: SerializeField] public int AmountGoldDropped;
    }
}
