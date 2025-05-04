using _Project.ConstructionBuildings;
using _Project.Enemy;
using _Project.ResourceExtraction.FishingRodMining;
using _Project.ResourceExtraction.OreMining;
using _Project.ResourceExtraction.ScissorsMining;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.ActionButton
{
    public class ActionButton : MonoBehaviour
    {
        [SerializeField] private Button _actionButton;
        [SerializeField] private Player.Player _player;

        private Collider2D[] _collider2D;

        public event Action<BuildingArea> OnStandingInConstructionZone;
        public event Action<Water> OnMiningFish;
        public event Action<Ore> OnMiningOre;
        public event Action<Grass> OnMiningGrass;

        private void Update() =>
            WithinVisibilityRadius(_player.Config.VisibilityRadius);

        private void OnEnable() =>
            _actionButton.onClick.AddListener(PerformAction);

        private void OnDisable() =>
            _actionButton.onClick.RemoveListener(PerformAction);

        private void PerformAction()
        {
            if (_collider2D == null)
                return;

            foreach (Collider2D collider in _collider2D)
            {
                if (collider.TryGetComponent(out BuildingArea buildingArea))
                {
                    if (buildingArea.IsZoneOccupied == false)
                    {
                        OnStandingInConstructionZone?.Invoke(buildingArea);
                        return;
                    }
                    else if (buildingArea.IsZoneOccupied)
                    {
                        buildingArea.BaseBuilding.Show();
                        return;
                    }
                }
                else if (collider.TryGetComponent(out SpawnEnemy spawnEnemy))
                {
                    if (spawnEnemy.IsSpawning)
                    {
                        spawnEnemy.DisableSpawner();
                        return;
                    }
                }
                else if (collider.TryGetComponent(out Ore ore))
                {
                    OnMiningOre?.Invoke(ore);
                    return;
                }
                else if (collider.TryGetComponent(out Water water))
                {
                    OnMiningFish?.Invoke(water);
                    return;
                }
                else if (collider.TryGetComponent(out Grass grass))
                {
                    OnMiningGrass?.Invoke(grass);
                    return;
                }
            }
        }

        private void WithinVisibilityRadius(float visibilityRadius) =>
            _collider2D = Physics2D.OverlapCircleAll(transform.position, visibilityRadius);
    }
}