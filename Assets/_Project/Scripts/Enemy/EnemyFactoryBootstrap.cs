using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core.HealthSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
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

        private EnemyFactory _enemyFactory;

        public EnemyFactory EnemyFactory => _enemyFactory;

        private void Awake()
        {
            _enemyFactory  = new EnemyFactory(_plantPredator, _slime, _experience, _coin, _healthInfoPrefab, 
                _healthViewPrefab, _dynamic, _layer);
        }
    }
}
