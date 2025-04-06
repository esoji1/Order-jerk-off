using Assets._Project.Sctipts.Inventory;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public abstract class BaseBuilding : MonoBehaviour
    {
        private GameObject _window;
        private Canvas _staticCanvas;

        private SpriteRenderer _spriteRenderer;
        private Type _type;
        private Button _exit;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Type Type => _type;
        public GameObject Window => _window;

        public virtual void Initialize(GameObject window, Canvas staticCanvas)
        {
            _window = window;
            _staticCanvas = staticCanvas;
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
