using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Core.HealthSystem
{
    public class HealthInfo : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarPrefab;

        private Canvas _healthUi;

        private Slider _instantiatedHealthBar;
        private HealthInfo _healthInfo;

        public Slider InstantiatedHealthBar => _instantiatedHealthBar;
        public HealthInfo GetHealthInfo => _healthInfo;

        public void Initialize(Canvas healthUi)
        {
            _healthInfo = this;
            _healthUi = healthUi;

            _instantiatedHealthBar = Instantiate(_healthBarPrefab, _healthUi.transform);
        }

        public void SetPositon(Transform transform)
        {
            Vector3 targetPosition = transform.position;
            _instantiatedHealthBar.transform.position = targetPosition;
        }
    }
}