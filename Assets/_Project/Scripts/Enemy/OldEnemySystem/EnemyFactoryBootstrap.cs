﻿//using _Project.Core.HealthSystem;
//using _Project.ScriptableObjects.Configs;
//using _Project.Weapon.Projectile;
//using System.Collections.Generic;
//using UnityEngine;

//namespace _Project.Enemy
//{
//    public class EnemyFactoryBootstrap : MonoBehaviour
//    {
//        [SerializeField] private ScriptableObjects.Configs.EnemyConfig _plantPredator, _slime, _magician, _heavyBlowEnemy;
//        [SerializeField] private SelectionGags.Experience _experience;
//        [SerializeField] private SelectionGags.Coin _coin;
//        [SerializeField] private HealthInfo _healthInfoPrefab;
//        [SerializeField] private HealthView _healthViewPrefab;
//        [SerializeField] private Canvas _dynamic;
//        [SerializeField] private LayerMask _layer;
//        [SerializeField] private Transform _mainBuildingPoint;
//        [SerializeField] private List<Transform> _points;
//        [SerializeField] private ProjectileEnemy _projectile;
//        [SerializeField] private Player.Player _player;
//        [SerializeField] private GameObject _circlePrimitiveHeavyAttack;

//        private EnemyFactory _enemyFactory;

//        public EnemyFactory EnemyFactory => _enemyFactory;
//        public List<Transform> Points => _points;

//        private void Awake()
//        {
//            _enemyFactory  = new EnemyFactory(_plantPredator, _slime, _magician, _heavyBlowEnemy, _experience, _coin, _healthInfoPrefab, 
//                _healthViewPrefab, _dynamic, _layer, _mainBuildingPoint, _projectile, _player, _circlePrimitiveHeavyAttack);
//        }
//    }
//}
