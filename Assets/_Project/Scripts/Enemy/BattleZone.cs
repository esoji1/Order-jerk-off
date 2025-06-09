using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BattleZone : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform[] _points;

        private EnemyFactoryBootstrap _enemyFactory;

        private BoxCollider2D _battleZone;

        private bool _isEnterZone;
        private SpawnEnemy[] _enemys;

        public bool IsEnterZone => _isEnterZone;

        private void Awake()
        {
            _battleZone = GetComponent<BoxCollider2D>();
            _enemys = GetComponentsInChildren<SpawnEnemy>();

            foreach (SpawnEnemy item in _enemys)
                item.Initialize(this, _enemyFactory, _points);
  
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

        public void Initialize(EnemyFactoryBootstrap enemyFactory)
        {
            _enemyFactory = enemyFactory;

            foreach (SpawnEnemy item in _enemys)
                item.Initialize(this, _enemyFactory, _points);
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