using Unity.Cinemachine;
using UnityEngine;
    
namespace Assets._Project.Sctipts.CameraMain
{
    public class AdaptingColliderResolution : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _zoneCamera;
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineConfiner2D _confiner;

        private void Awake() => ResizeCollider();
        public bool isY;
        public bool isX;

        public float value;

        private void Update()
        {
            if (isY == false)
            {
                AddTopHeight(value);
                isY = true;
            }
            else if(isX == false)
            {
                AddBottomHeight(value);
                isX = true;
            }
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
    }
}