using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.ResourceExtraction
{
    public class PercentageFillView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _prefab;

        private TextMeshProUGUI _percentageFill;
        private Coroutine _timerCoroutine;
        private float _duration;
        private float _elapsedTime;

        public void StartTimer(float duration, Transform targetTransform, Canvas canvas)
        {
            _duration = duration;
            _elapsedTime = 0f;

            SpawnPercentage(targetTransform, canvas);

            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            _timerCoroutine = StartCoroutine(UpdatePercentage());
        }

        public void StopTimer()
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            _elapsedTime = 0f;

            if (_percentageFill != null)
                Destroy(_percentageFill.gameObject);
        }

        private void SpawnPercentage(Transform targetTransform, Canvas canvas)
        {
            if (_percentageFill != null)
                Destroy(_percentageFill.gameObject);

            _percentageFill = Instantiate(_prefab, canvas.transform);
            _percentageFill.transform.position = targetTransform.position + new Vector3(0, 0.7f, 0);
        }

        private IEnumerator UpdatePercentage()
        {
            while (_elapsedTime < _duration)
            {
                _elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp01(_elapsedTime / _duration);
                _percentageFill.text = $"{(int)(progress * 100)}%";
                yield return null;
            }

            _percentageFill.text = "100%";

            yield return new WaitForSeconds(0.5f);
            StopTimer();
        }
    }
}