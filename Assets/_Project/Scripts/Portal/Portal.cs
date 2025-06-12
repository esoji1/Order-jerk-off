using _Project.CameraMain;
using _Project.MapGeneration;
using System.Collections;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Portal
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Button _portal;
        [SerializeField] private AdaptingColliderResolution _adaptingColliderResolution;
        [SerializeField] private TransitionLevel _transitionLevel;
        [SerializeField] private float _delayBetweenTeleportations;
        [SerializeField] private MapGenerationController _mapGenerationController;
        [SerializeField] private Player.Player _player;

        private Coroutine _coroutine;
        private bool _isTeleportations;

        private void OnEnable()
        {
            _portal.onClick.AddListener(MoveSpawn);
        }

        private void OnDisable()
        {
            _portal.onClick.RemoveListener(MoveSpawn);
        }

        private void MoveSpawn()
        {
            if (_isTeleportations == false)
            {
                _player.transform.position = new Vector3(0, -6.5f, 0);
                _adaptingColliderResolution.StretchVerticallyBetweenPoints(_transitionLevel.BackPointA.position, _transitionLevel.BackPointB.position);
                _coroutine = StartCoroutine(Teleportations());
                _mapGenerationController.DestroyAllMap();
            }
        }

        private IEnumerator Teleportations()
        {
            _isTeleportations = true;
            yield return new WaitForSeconds(_delayBetweenTeleportations);
            _isTeleportations = false;
        }
    }
}