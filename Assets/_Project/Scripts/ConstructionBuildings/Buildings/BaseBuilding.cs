using _Project.Core;
using _Project.Core.Interface;
using _Project.ScriptableObjects.Configs;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.ConstructionBuildings.Buildings
{
    public abstract class BaseBuilding : MonoBehaviour, IDamage
    {
        private GameObject _window;
        private Canvas _staticCanvas;
        private Player.Player _player;
        private BuildsConfig _config;

        private Health _health;

        private SpriteRenderer _spriteRenderer;
        private Button _exit;
        private Type _type;

        private BuildingArea _buildingArea;
        private Tween _tween;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Type Type => _type;
        public GameObject Window => _window;
        public Player.Player Player => _player;
        public BuildingArea BuildingArea => _buildingArea;
        public BuildsConfig Config => _config;

        public virtual void Initialize(BuildsConfig config, Canvas staticCanvas, Player.Player player)
        {
            _config = config;
            _staticCanvas = staticCanvas;
            _player = player;
            _health = new Health(Config.Health);

            _window = Instantiate(Config.WindowPrefab, _staticCanvas.transform);
            _window.SetActive(false);

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _exit = _window.GetComponentInChildren<Exit>().GetComponent<Button>();
            _type = GetType();

            _exit.onClick.AddListener(Hide);
            _health.OnDie += Die;
        }

        public void SetBuildingArea(BuildingArea buildingArea) => _buildingArea = buildingArea;

        public void Show()
        {
            _window.SetActive(true);
            _tween = _window.transform
                .DOScale(1, 0.5f);
        }

        public void Hide()
        {
            _tween.Kill();

            _window.SetActive(false);
            _window.transform.localScale = new Vector3(0, 0, 0);
        }

        public void Damage(int damage) => _health.TakeDamage(damage);

        private void Die()
        {
            _buildingArea.SetZoneOccupeid(false);
            _buildingArea.SetBaseBuilding(null);
            _tween.Kill();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _exit.onClick.RemoveListener(Hide);
            _health.OnDie -= Die;
            Destroy(_window.gameObject);
        }
    }
}
