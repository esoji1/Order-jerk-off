using _Project.Core.HealthSystem;
using _Project.ScriptableObjects;
using _Project.SelectionGags;
using UnityEngine;  

namespace _Project.Enemy
{
    public class EnemyFactoryBootstrap : MonoBehaviour
    {
        [Header("All")]
        [SerializeField] private EnemyConfig _planet, _slime, _distant, _heavy, _orc, _wizard, _summoner;
        [Header("MoveToTarget")]
        [SerializeField] private EnemyConfig _planetMoveToTarget, _slimeMoveToTarget, _distantMoveToTarget, _heavyMoveToTarget, 
            _wizardMoveToTarget;
        [SerializeField] private HealthInfo _healthInfoPrefab;
        [SerializeField] private HealthView _healthViewPrefab;
        [SerializeField] private Canvas _uiDynamic;
        [SerializeField] private Experience _experiencePrefab;
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private Player.Player _player;
        [SerializeField] private GivesData _givesData;
        [SerializeField] private Transform _targetForMoveToTarget;

        private EnemyFactory _enemyFactory;

        public EnemyFactory EnemyFactory => _enemyFactory;

        private void Awake()
        {
            _enemyFactory = new EnemyFactory(_planet, _slime, _distant, _heavy, _wizard, _summoner, _orc, _planetMoveToTarget,
                _slimeMoveToTarget, _distantMoveToTarget, _heavyMoveToTarget, _wizardMoveToTarget, _healthInfoPrefab, 
                _healthViewPrefab, _uiDynamic, _experiencePrefab, _coinPrefab, _player, _givesData, _targetForMoveToTarget);
        }
    }
}