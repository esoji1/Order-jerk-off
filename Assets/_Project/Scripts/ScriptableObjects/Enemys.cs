using System.Collections.Generic;
using UnityEngine;

namespace _Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemys", menuName = "ScriptableObjects/Enemys")]
    public class Enemys : ScriptableObject
    {
        [field: SerializeField] public List<Enemy.Behaviors.Enemy> GetEnemys;
    }
}
