using _Project.Quests.KillQuest;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = "IconEnemyData", menuName = "ScriptableObjects/Configs/IconEnemy/IconEnemyData")]
    public class IconEnemyData : ScriptableObject
    {
        [field: SerializeField] public List<IconEnemy> ListIconEnemy { get; private set; }
    }
}
