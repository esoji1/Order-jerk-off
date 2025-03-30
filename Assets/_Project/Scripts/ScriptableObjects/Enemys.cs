using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemys", menuName = "ScriptableObjects/Enemys")]
    public class Enemys : ScriptableObject
    {
        [field: SerializeField] public List<Enemy.Enemy> GetEnemys;
    }
}
