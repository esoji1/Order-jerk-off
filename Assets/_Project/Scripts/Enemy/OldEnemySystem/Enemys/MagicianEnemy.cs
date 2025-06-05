//using _Project.Core.HealthSystem;
//using _Project.Enemy.Attakcs.Interface;
//using _Project.ScriptableObjects.Configs;
//using _Project.Weapon.Projectile;
//using System.Collections.Generic;
//using UnityEngine;

//namespace _Project.Enemy.Enemys
//{
//    public class MagicianEnemy : Enemy, IRanged
//    {
//        private ProjectileEnemy _projectile;

//        public ProjectileEnemy ProjectileEnemy => _projectile;

//        public void Initialize(ScriptableObjects.Configs.EnemyConfig config, SelectionGags.Experience prefabExperience, SelectionGags.Coin prefabCoin,
//            HealthInfo healthInfoPrefab, HealthView healthViewPrefab, Canvas dynamic, LayerMask layer, List<Transform> points, 
//            bool isMoveRandomPoints, Transform mainBuildingPoint, EnemyTypes enemyTypes, Player.Player player, ProjectileEnemy projectileEnemy)
//        {
//            base.Initialize(config, prefabExperience, prefabCoin, healthInfoPrefab, healthViewPrefab, dynamic, layer, points,
//                isMoveRandomPoints, mainBuildingPoint, enemyTypes, player);

//            _projectile = projectileEnemy;
//        }
//    }
//}
