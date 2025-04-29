using Assets._Project.Scripts.Enemy;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Configs/EnemySpawner/EnemyGroup", fileName = "EnemyGroup")]
    public class EnemyGroup : ScriptableObject
    {
        [field: SerializeField] public EnemyTypes EnemyTypes { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
        [field: SerializeField] public float SpawnInterval { get; private set; }
    }
}
