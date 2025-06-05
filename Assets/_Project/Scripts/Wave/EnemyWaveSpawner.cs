using _Project.Enemy;
using _Project.ScriptableObjects.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Project.Wave
{
    public class EnemyWaveSpawner : MonoBehaviour
    {
        private List<ScriptableObjects.Configs.Wave> _waves;
        private EnemyFactoryBootstrap _bootstrapEnemy;
        private float _timeBetweenWaves;
        private TimerBetweenWavesView _timerBetweenWavesView;

        private WaveView _waveView;

        private List<Enemy.Enemy> _activeEnemies = new();
        private int _currentWaveIndex = 0;
        private bool _isSpawning = false;

        public TimerBetweenWavesView TimerBetweenWavesView => _timerBetweenWavesView;
        public float TimeBetweenWaves => _timeBetweenWaves;

        public event Action OnWin;

        public void Initialize(List<ScriptableObjects.Configs.Wave> waves, EnemyFactoryBootstrap bootstrapEnemy,
            float timeBetweenWaves, TimerBetweenWavesView timerBetweenWavesView, TextMeshProUGUI waveText)
        {
            _waves = waves;
            _bootstrapEnemy = bootstrapEnemy;
            _timeBetweenWaves = timeBetweenWaves;
            _timerBetweenWavesView = timerBetweenWavesView;

            _waveView = new WaveView(waveText, _waves.Count);
        }

        public void StartEnemyWaveSpawner() =>
            StartCoroutine(SpawnWaves());

        private IEnumerator SpawnWaves()
        {
            _waveView.Show(_currentWaveIndex);

            while (_currentWaveIndex < _waves.Count)
            {
                if (_isSpawning == false)
                {
                    _isSpawning = true;

                    if (_currentWaveIndex == 0)
                    {
                        _timerBetweenWavesView.StartTimeBeetwenWaves(_timeBetweenWaves);

                        yield return new WaitForSeconds(_timeBetweenWaves);
                        yield return StartCoroutine(SpawnWave(_waves[_currentWaveIndex]));
                    }

                    if (_currentWaveIndex > 0)
                        yield return StartCoroutine(SpawnWave(_waves[_currentWaveIndex]));

                    _isSpawning = false;

                    yield return new WaitUntil(() => _activeEnemies.Count == 0);

                    _currentWaveIndex++;

                    if (_currentWaveIndex < _waves.Count)
                    {
                        _waveView.Show(_currentWaveIndex);

                        _timerBetweenWavesView.StartTimeBeetwenWaves(_timeBetweenWaves);
                        yield return new WaitForSeconds(_timeBetweenWaves);
                    }
                }
            }

            OnWin?.Invoke();
        }


        private IEnumerator SpawnWave(ScriptableObjects.Configs.Wave wave)
        {
            foreach (EnemyGroup group in wave.EnemyGroups)
            {
                for (int i = 0; i < group.Count; i++)
                {
                    Enemy.Enemy newEnemy = _bootstrapEnemy.EnemyFactory.Get(group.EnemyTypes, 
                        _bootstrapEnemy.Points[UnityEngine.Random.Range(0, _bootstrapEnemy.Points.Length)].position);

                    _activeEnemies.Add(newEnemy);
                    newEnemy.OnEnemyDie += HandleEnemyDeath;

                    yield return new WaitForSeconds(group.SpawnInterval);
                }
            }
        }

        private void HandleEnemyDeath(Enemy.Enemy enemy)
        {
            if (_activeEnemies.Contains(enemy))
                _activeEnemies.Remove(enemy);
        }
    }
}