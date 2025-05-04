using _Project.Core.HealthSystem;
using _Project.ScriptableObjects.Configs;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Enemy
{
    public class EnemyFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _plantPredator, _slime;
        [SerializeField] private SelectionGags.Experience _experience;
        [SerializeField] private SelectionGags.Coin _coin;
        [SerializeField] private HealthInfo _healthInfoPrefab;
        [SerializeField] private HealthView _healthViewPrefab;
        [SerializeField] private Canvas _dynamic;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Transform _mainBuildingPoint;
        [SerializeField] private List<Transform> _points;

        private EnemyFactory _enemyFactory;

        public EnemyFactory EnemyFactory => _enemyFactory;
        public List<Transform> Points => _points;

        private void Awake()
        {
            _enemyFactory  = new EnemyFactory(_plantPredator, _slime, _experience, _coin, _healthInfoPrefab, 
                _healthViewPrefab, _dynamic, _layer, _mainBuildingPoint);
        }
    }
}
