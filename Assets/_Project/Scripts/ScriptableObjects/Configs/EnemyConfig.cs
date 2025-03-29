using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public Enemy.Enemy Prefab;
        [field: SerializeField] public int Health;
        [field: SerializeField] public int Damage;
        [field: SerializeField] public int Speed;
        [field: SerializeField] public float AttackRadius;
        [field: SerializeField] public float SpeedAttack;
        [field: SerializeField] public float AmountExperienceDropped;
        [field: SerializeField] public float AmountGoldDropped;
    }
}
