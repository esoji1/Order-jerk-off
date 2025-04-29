using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Configs/EnemySpawner/Wave", fileName = "Wave")]
    public class Wave : ScriptableObject
    {
        [field: SerializeField] public List<EnemyGroup> EnemyGroups { get; private set; }
    }
}