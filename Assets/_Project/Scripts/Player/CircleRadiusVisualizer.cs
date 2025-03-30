using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class CircleRadiusVisualizer : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Material _material;
        [SerializeField] private Color _radiusColor;
        [SerializeField] private float _lineWidth;
        [SerializeField] private int _numberPoints;

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

        private void Update() => DrawRadius();

        private void DrawRadius()
        {
            float step = 360f / _numberPoints;

            for (int i = 0; i < _numberPoints; i++)
            {
                float angle = step * i * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * _player.Config.AttackRadius;
                float y = Mathf.Sin(angle) * _player.Config.AttackRadius;
                Vector3 point = new Vector3(x, y, 0f);
                _lineRenderer.SetPosition(i, _player.transform.position + point);
            }

            _lineRenderer.SetPosition(_numberPoints, _lineRenderer.GetPosition(0));
        }
    }
}
