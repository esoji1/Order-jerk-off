using _Project.CameraMain;
using System;
using UnityEngine;

namespace _Project.MapGeneration
{
    public class TransitionLevel : MonoBehaviour
    {
        [SerializeField] private AdaptingColliderResolution _adaptingColliderResolution;
        [SerializeField] private Transform _startPointA;
        [SerializeField] private Transform _startPointB;
        [SerializeField] private Transform _backPointA;
        [SerializeField] private Transform _backtPointB;
        [SerializeField] private Player.Player _player;

        private float _addPositionPlayer = 2f;

        public event Action OnForward;
        public event Action OnBack;

        public Transform StartPointA => _startPointA;
        public Transform StartPointB => _startPointB;
        public Transform BackPointA => _backPointA;
        public Transform BackPointB => _backtPointB;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            float deltaY = collision.bounds.center.y - GetComponent<Collider2D>().bounds.center.y;

            if (deltaY > 0)
            {
                Debug.Log("Âîøåë ÑÂÅÐÕÓ");
                if (collision.TryGetComponent(out Player.Player player))
                {
                    OnBack?.Invoke();

                    _player.transform.position -= new Vector3(0f, _addPositionPlayer, 0f);
                    _adaptingColliderResolution.StretchVerticallyBetweenPoints(_backPointA.position, _backtPointB.position);
                }
            }
            else
            {
                Debug.Log("Âîøåë ÑÍÈÇÓ");

                if (collision.TryGetComponent(out Player.Player player))
                {
                    OnForward?.Invoke();

                    if (_startPointA != null && _startPointB != null)
                    {
                        _player.transform.position += new Vector3(0f, _addPositionPlayer, 0f);
                        _adaptingColliderResolution.StretchVerticallyBetweenPoints(_startPointA.position, _startPointB.position);
                    }
                }
            }
        }

        public void Initialize(AdaptingColliderResolution adaptingColliderResolution, Player.Player player)
        {
            _adaptingColliderResolution = adaptingColliderResolution;
            _player = player;
        }

        public void SetStartPoints(Transform pointA, Transform pointB)
        {
            _startPointA = pointA;
            _startPointB = pointB;

            _player.transform.position += new Vector3(0f, _addPositionPlayer, 0f);
            _adaptingColliderResolution.StretchVerticallyBetweenPoints(_startPointA.position, _startPointB.position);
        }

        public void SetBeckPoints(Transform pointA, Transform pointB)
        {
            _backPointA = pointA;
            _backtPointB = pointB;
        }
    }
}