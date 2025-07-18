﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _Project.Wave
{
    public class TimerBetweenWavesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeBetweenWaves;

        public event Action<float> OnStartTimer;

        public void StartTimeBeetwenWaves(float timeBetweenWaves) =>
            StartCoroutine(StartCountdown(timeBetweenWaves));

        public void Show() => _timeBetweenWaves.gameObject.SetActive(true);
        public void Hide() => _timeBetweenWaves.gameObject.SetActive(false);

        private IEnumerator StartCountdown(float timeBetweenWaves)
        {
            OnStartTimer?.Invoke(timeBetweenWaves);

            Show();
            float remainingTime = timeBetweenWaves;

            while (remainingTime > 0)
            {
                _timeBetweenWaves.text = Mathf.CeilToInt(remainingTime).ToString();
                yield return new WaitForSeconds(1f);
                remainingTime -= 1f;
            }

            Hide();
            _timeBetweenWaves.text = "0";
        }
    }
}
