using Unity.Cinemachine;
using UnityEngine;

namespace Assets._Project.Sctipts.CameraMain
{
    [ExecuteAlways]
    public class FixedCameraSize : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _CinemachineCamera;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _maxWidth = 5f;

        private void Start() => AdjustCamera();

        private void Update() => AdjustCamera();

        private void AdjustCamera()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            float cameraWidth = 2f * _camera.orthographicSize * (screenWidth / (float)screenHeight);

            if (cameraWidth > _maxWidth)
                _CinemachineCamera.Lens.OrthographicSize = _maxWidth / (2f * (screenWidth / (float)screenHeight));
            else
                _CinemachineCamera.Lens.OrthographicSize = _maxWidth / (2f * (screenWidth / (float)screenHeight));
        }
    }
}