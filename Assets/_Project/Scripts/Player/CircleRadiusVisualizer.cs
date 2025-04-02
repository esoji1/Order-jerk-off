using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class CircleRadiusVisualizer : MonoBehaviour
    {
        [SerializeField] private Color _radiusColor;
        [SerializeField] private Material _material;
        [SerializeField, Range(0f, 0.1f)] private float _lineWidth;
        [SerializeField] private int _numberPoints;

        private Transform _transform;
        
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.material = _material;
            _lineRenderer.startWidth = _lineWidth;
            _lineRenderer.endWidth = _lineWidth;
            _lineRenderer.positionCount = _numberPoints + 1;
            _material.color = _radiusColor;
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public void DrawRadius(float radius)
        {
            float step = 360f / _numberPoints;

            for (int i = 0; i < _numberPoints; i++)
            {
                float angle = step * i * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                Vector3 point = new Vector3(x, y, 0f);
                _lineRenderer.SetPosition(i, _transform.position + point);
            }

            _lineRenderer.SetPosition(_numberPoints, _lineRenderer.GetPosition(0));
        }
    }
}
