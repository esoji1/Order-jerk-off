using _Project.Core.HealthSystem;
using _Project.ScriptableObjects;
using _Project.SelectionGags;
using UnityEngine;  

namespace _Project.Enemy
{
    public class EnemyFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _planet, _slime, _distant, _heavy, _wizard;
        [SerializeField] private HealthInfo _healthInfoPrefab;
        [SerializeField] private HealthView _healthViewPrefab;
        [SerializeField] private Canvas _uiDynamic;
        [SerializeField] private Experience _experiencePrefab;
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private Transform[] _points;
        [SerializeField] private Player.Player _player;
        [SerializeField] private GivesData _givesData;

        private EnemyFactory _enemyFactory;

        public EnemyFactory EnemyFactory => _enemyFactory;
        public Transform[] Points => _points;

        private void Awake()
        {
            _enemyFactory = new EnemyFactory(_planet, _slime, _distant, _heavy, _wizard, _healthInfoPrefab, _healthViewPrefab, _uiDynamic, _experiencePrefab, _coinPrefab,
                _points, _player, _givesData);
        }
    }
}