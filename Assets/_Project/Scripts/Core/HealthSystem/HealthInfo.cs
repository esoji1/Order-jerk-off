using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Core.HealthSystem
{
    public class HealthInfo : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarPrefab;

        private Canvas _healthUi;

        private Slider _instantiatedHealthBar;
        private HealthInfo _healthInfo;
        private TextMeshProUGUI _textHp;

        public Slider InstantiatedHealthBar => _instantiatedHealthBar;
        public HealthInfo GetHealthInfo => _healthInfo;
        public TextMeshProUGUI TextHp => _textHp;
        
        public void Initialize(Canvas healthUi)
        {
            _healthInfo = this;
            _healthUi = healthUi;

            _instantiatedHealthBar = Instantiate(_healthBarPrefab, _healthUi.transform);
            _textHp = _instantiatedHealthBar.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetPositon(Transform transform)
        {
            Vector3 targetPosition = transform.position;
            _instantiatedHealthBar.transform.position = targetPosition;
        }
    }
}