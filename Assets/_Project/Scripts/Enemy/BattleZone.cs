using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BattleZone : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private BoxCollider2D _battleZone;

        private bool _isEnterZone;
        private SpawnEnemy[] _enemys;

        public bool IsEnterZone => _isEnterZone;

        private void Awake()
        {
            _battleZone = GetComponent<BoxCollider2D>();
            _enemys = GetComponentsInChildren<SpawnEnemy>();

            foreach (SpawnEnemy item in _enemys)
                item.Initialize(this);

            ResizeCollider();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player _))
                _isEnterZone = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player _))
                _isEnterZone = false;
        }

        private void ResizeCollider()
        {
            if (_battleZone == null || _camera == null) return;

            float screenWidthWorld = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            float screenLeftWorld = _camera.ScreenToWorldPoint(Vector3.zero).x;

            float colliderWidth = screenWidthWorld - screenLeftWorld;
            _battleZone.size = new Vector2(colliderWidth + 0.1f, _battleZone.size.y);
        }
    }
}