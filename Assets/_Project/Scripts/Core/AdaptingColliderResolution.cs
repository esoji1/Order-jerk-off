using UnityEngine;
    
namespace Assets._Project.Sctipts.Core
{
    public class AdaptingColliderResolution : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _zoneCamera;
        [SerializeField] private Camera _camera;

        private void Awake() => ResizeCollider();

        private void ResizeCollider()
        {
            if (_zoneCamera == null || _camera == null) return;

            float screenWidthWorld = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            float screenLeftWorld = _camera.ScreenToWorldPoint(Vector3.zero).x;

            float colliderWidth = screenWidthWorld - screenLeftWorld;
            _zoneCamera.size = new Vector2(colliderWidth + 0.1f, _zoneCamera.size.y);
        }
    }
}