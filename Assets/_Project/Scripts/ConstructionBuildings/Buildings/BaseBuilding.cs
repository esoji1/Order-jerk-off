using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public abstract class BaseBuilding : MonoBehaviour
    {
        private GameObject _window;
        private Canvas _staticCanvas;
        private Player.Player _player;

        private SpriteRenderer _spriteRenderer;
        private Type _type;
        private Button _exit;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Type Type => _type;
        public GameObject Window => _window;
        public Player.Player Player => _player;

        public virtual void Initialize(GameObject window, Canvas staticCanvas, Player.Player player)
        {
            _window = window;
            _staticCanvas = staticCanvas;
            _player = player;
            _window = Instantiate(window, _staticCanvas.transform);
            _window.SetActive(false);

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _exit = _window.GetComponentInChildren<Button>();
            _type = GetType();

            _exit.onClick.AddListener(Hide);
        }

        public void Show() => _window.SetActive(true);
        public void Hide() => _window.SetActive(false);

        private void OnDestroy()
        {
            _exit.onClick.RemoveListener(Hide);
            Destroy(_window);
        }
    }
}
