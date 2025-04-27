using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public Enemy.Enemy Prefab;
        [field: SerializeField] public int Health;
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float AttackRadius;
        [field: SerializeField] public float VisibilityRadius;
        [field: SerializeField] public int AmountExperienceDropped;
        [field: SerializeField] public int AmountGoldDropped;
        [SerializeField] public int Damage;
    }
}
