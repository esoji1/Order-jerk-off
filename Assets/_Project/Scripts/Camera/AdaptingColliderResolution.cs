using Unity.Cinemachine;
using UnityEngine;

namespace _Project.CameraMain
{
    public class AdaptingColliderResolution : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _zoneCamera;
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineConfiner2D _confiner;

        private Vector2 _originalSize;
        private Vector2 _originalOffset;
        private bool _hasOriginalValues = false;

        private void Awake()
        {
            _originalSize = _zoneCamera.size;
            _originalOffset = _zoneCamera.offset;
            _hasOriginalValues = true;

            ResizeCollider();
        }

        private void ResizeCollider()
        {
            if (_zoneCamera == null || _camera == null) return;

            float screenWidthWorld = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            float screenLeftWorld = _camera.ScreenToWorldPoint(Vector3.zero).x;

            float colliderWidth = screenWidthWorld - screenLeftWorld;
            _zoneCamera.size = new Vector2(colliderWidth + 0.01f, _zoneCamera.size.y);
        }

        public void AddTopHeight(float delta)
        {
            if (_zoneCamera == null) return;

            _zoneCamera.size = new Vector2(_zoneCamera.size.x, _zoneCamera.size.y + delta);
            _zoneCamera.offset = new Vector2(_zoneCamera.offset.x, _zoneCamera.offset.y + delta * 0.5f);

            _confiner.InvalidateBoundingShapeCache();
        }

        public void AddBottomHeight(float delta)
        {
            if (_zoneCamera == null) return;

            _zoneCamera.size = new Vector2(_zoneCamera.size.x, _zoneCamera.size.y + delta);
            _zoneCamera.offset = new Vector2(_zoneCamera.offset.x, _zoneCamera.offset.y - delta * 0.5f);

            _confiner.InvalidateBoundingShapeCache();
        }

        public void ResetToDefault()
        {
            if (_zoneCamera == null || !_hasOriginalValues) return;

            _zoneCamera.size = _originalSize;
            _zoneCamera.offset = _originalOffset;

            if (_confiner != null)
                _confiner.InvalidateBoundingShapeCache();

            ResizeCollider();
        }

        public void StretchVerticallyBetweenPoints(Vector2 worldBottom, Vector2 worldTop)
        {
            Transform parent = _zoneCamera.transform.parent;

            Vector2 localBottom = parent.InverseTransformPoint(worldBottom);
            Vector2 localTop = parent.InverseTransformPoint(worldTop);

            float height = localTop.y - localBottom.y;

            Vector2 size = _zoneCamera.size;
            size.y = height;
            _zoneCamera.size = size;

            Vector2 offset = _zoneCamera.offset;
            offset.y = (localTop.y + localBottom.y) / 2f;
            _zoneCamera.offset = offset;

            _confiner.InvalidateBoundingShapeCache();
        }
    }
}