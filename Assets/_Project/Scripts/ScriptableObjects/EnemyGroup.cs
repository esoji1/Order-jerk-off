using _Project.Enemy;
using UnityEngine;

namespace _Project.ScriptableObjects.Configs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Configs/EnemySpawner/EnemyGroup", fileName = "EnemyGroup")]
    public class EnemyGroup : ScriptableObject
    {
        [field: SerializeField] public EnemyType EnemyTypes { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
        [field: SerializeField] public float SpawnInterval { get; private set; }
    }
}
