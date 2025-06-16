using _Project.Enemy;
using _Project.ScriptableObjects.Configs;
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
        private Transform[] _pointsWave;

        private WaveView _waveView;

        private List<Enemy.Behaviors.Enemy> _activeEnemies = new();
        private int _currentWaveIndex = 0;
        private bool _isSpawning = false;

        public TimerBetweenWavesView TimerBetweenWavesView => _timerBetweenWavesView;
        public float TimeBetweenWaves => _timeBetweenWaves;

        public void Initialize(List<ScriptableObjects.Configs.Wave> waves, EnemyFactoryBootstrap bootstrapEnemy,
            float timeBetweenWaves, TimerBetweenWavesView timerBetweenWavesView, TextMeshProUGUI waveText, Transform[] pointsWave)
        {
            _waves = waves;
            _bootstrapEnemy = bootstrapEnemy;
            _timeBetweenWaves = timeBetweenWaves;
            _timerBetweenWavesView = timerBetweenWavesView;
            _pointsWave = pointsWave;

            _waveView = new WaveView(waveText);
        }

        public void StartEnemyWaveSpawner() =>
            StartCoroutine(SpawnWaves());

        private IEnumerator SpawnWaves()
        {


            //while (_currentWaveIndex < _waves.Count)
            while (true)
            {
                if (_isSpawning == false)
                {
                    int randomWave = Random.Range(0, _waves.Count);
                    _isSpawning = true;

                    //if (_currentWaveIndex == 0)
                    //{
                    _waveView.Show(_currentWaveIndex + 1);
                    _timerBetweenWavesView.StartTimeBeetwenWaves(_timeBetweenWaves);

                    yield return new WaitForSeconds(_timeBetweenWaves);
                    yield return StartCoroutine(SpawnWave(_waves[randomWave]));
                    //}

                    //if (_currentWaveIndex > 0)
                    //    yield return StartCoroutine(SpawnWave(_waves[randomWave]));

                    _isSpawning = false;

                    yield return new WaitUntil(() => _activeEnemies.Count == 0);

                    _currentWaveIndex++;

                    //if (_currentWaveIndex < _waves.Count)
                    //{
                    //    _waveView.Show(_currentWaveIndex + 1);

                    //    _timerBetweenWavesView.StartTimeBeetwenWaves(_timeBetweenWaves);
                    //    yield return new WaitForSeconds(_timeBetweenWaves);
                    //}
                }
            }
        }


        private IEnumerator SpawnWave(ScriptableObjects.Configs.Wave wave)
        {
            foreach (EnemyGroup group in wave.EnemyGroups)
            {
                for (int i = 0; i < group.Count; i++)
                {
                    Enemy.Behaviors.Enemy newEnemy = _bootstrapEnemy.EnemyFactory.Get(group.EnemyTypes,
                        _pointsWave[UnityEngine.Random.Range(0, _pointsWave.Length)].position, null);

                    _activeEnemies.Add(newEnemy);
                    newEnemy.OnEnemyDie += HandleEnemyDeath;

                    yield return new WaitForSeconds(group.SpawnInterval);
                }
            }
        }

        private void HandleEnemyDeath(Enemy.Behaviors.Enemy enemy)
        {
            if (_activeEnemies.Contains(enemy))
                _activeEnemies.Remove(enemy);
        }
    }
}