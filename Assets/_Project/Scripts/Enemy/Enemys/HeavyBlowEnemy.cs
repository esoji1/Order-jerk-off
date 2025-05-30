using _Project.Core.HealthSystem;
using _Project.Enemy.Attakcs.Interface;
using _Project.ScriptableObjects.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Enemy.Enemys
{
    public class HeavyBlowEnemy : Enemy, IHeavyAttack
    {
        private GameObject _circlePrimitiveHeavyAttack;

        public GameObject CirclePrimitiveHeavyAttack => _circlePrimitiveHeavyAttack;

        public void Initialize(EnemyConfig config, SelectionGags.Experience prefabExperience, SelectionGags.Coin prefabCoin,
            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas dynamic, LayerMask layer, List<Transform> points,
            bool isMoveRandomPoints, Transform mainBuildingPoint, EnemyTypes enemyTypes, Player.Player player, GameObject circlePrimitiveHeavyAttack)
        {
            base.Initialize(config, prefabExperience, prefabCoin, healthInfoPrefab, healthViewPrefab, dynamic, layer, points,
               isMoveRandomPoints, mainBuildingPoint, enemyTypes, player);

            _circlePrimitiveHeavyAttack = circlePrimitiveHeavyAttack;
        }
    }
}
