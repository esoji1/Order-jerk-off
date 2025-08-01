using _Project.Enemy;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Project.Wave
{
    public class BootstrapEnemyWaveSpawner : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObjects.Configs.Wave> _waves;
        [SerializeField] private _Project.Enemy.EnemyFactoryBootstrap _bootstrapEnemy;
        [SerializeField] private float _timeBetweenWaves = 20f;
        [SerializeField] private TimerBetweenWavesView _timerBetweenWavesView;
        [SerializeField] private EnemyWaveSpawner _enemyWaveSpawner;
        [SerializeField] private TextMeshProUGUI _waveText;
        [SerializeField] private Transform[] _pointsWave;

        public EnemyWaveSpawner EnemyWaveSpawner => _enemyWaveSpawner;

        private void Awake()
        {
            _enemyWaveSpawner.Initialize(_waves, _bootstrapEnemy, _timeBetweenWaves, _timerBetweenWavesView, _waveText, _pointsWave);
            _enemyWaveSpawner.StartEnemyWaveSpawner();
        }
    }
}