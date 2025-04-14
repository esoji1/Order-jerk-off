using NUnit.Framework.Constraints;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.ResourceExtraction
{
    public class PercentageFillView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _prefab;

        private TextMeshProUGUI _percentageFill;
        private float _addPercent;
        private float _result;


        public void StartTimer(float second, Transform transform, Canvas canvas)
        {
            SpawnPercentage(transform, canvas);
            _addPercent = 100 / second;
            StartCoroutine(StrictOneSecondTimer());
        }

        public void StopTimer()
        {
            StopCoroutine(StrictOneSecondTimer());
            _result = 0;
            Destroy(_percentageFill.gameObject);
        }

        private void SpawnPercentage(Transform transform, Canvas canvas)
        {
            _percentageFill = Instantiate(_prefab, canvas.transform);

            Vector3 worldPosition = transform.position;
            _percentageFill.transform.position = worldPosition;
            _percentageFill.transform.position += new Vector3(0, 0.7f, 0);
        }

        private IEnumerator StrictOneSecondTimer()
        {
            while (_result < 100)
            {
                yield return new WaitForSeconds(1f);
                _result += _addPercent;
                _percentageFill.text = $"{(int)_result}%";
            }
        }
    }
}