using Assets._Project.Scripts.Enemy;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Wave
{
    public class BootstrapEnemyWaveSpawner : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObjects.Configs.Wave> _waves;
        [SerializeField] private EnemyFactoryBootstrap _bootstrapEnemy;
        [SerializeField] private float _timeBetweenWaves = 20f;
        [SerializeField] private TimerBetweenWavesView _timerBetweenWavesView;
        [SerializeField] private EnemyWaveSpawner _enemyWaveSpawner;
        [SerializeField] private TextMeshProUGUI _waveText;

        public EnemyWaveSpawner EnemyWaveSpawner => _enemyWaveSpawner;

        private void Awake()
        {
            _enemyWaveSpawner.Initialize(_waves, _bootstrapEnemy, _timeBetweenWaves, _timerBetweenWavesView, _waveText);
            _enemyWaveSpawner.StartEnemyWaveSpawner();
        }
    }
}