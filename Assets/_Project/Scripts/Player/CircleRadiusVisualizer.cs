using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

namespace Assets._Project.Scripts.Player
{
    [RequireComponent(typeof(LineRenderer))]

    public class CircleRadiusVisualizer : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private int _segments = 64;

        private LineRenderer _lineRenderer;

        private void Start()
        {
            if (_lineRenderer == null)
                _lineRenderer = GetComponent<LineRenderer>();

            DrawCircle();
        }

        private void Update()
        {
            UpdateRadius();
        }

        private void DrawCircle()
        {
            _lineRenderer.positionCount = _segments + 1; // +1 дл€ замыкани€ круга

            float deltaTheta = (2f * Mathf.PI) / _segments;
            float theta = 0f;

            for (int i = 0; i < _segments + 1; i++)
            {
                float x = _player.Config.AttackRadius * Mathf.Cos(theta);
                float z = _player.Config.AttackRadius * Mathf.Sin(theta);
                _lineRenderer.SetPosition(i, new Vector3(x, 0.1f, z)); // 0.1f Ч небольша€ высота над землЄй
                theta += deltaTheta;
            }
        }

        public void UpdateRadius()
        {
            DrawCircle();
        }
    }
}