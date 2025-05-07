using _Project.ConstructionBuildings.Buildings;
using UnityEngine;

namespace _Project.ConstructionBuildings
{
    public class BuildingArea : MonoBehaviour
    {
        [SerializeField] private int _leftOrRightSide;

        private bool _isZoneOccupied;
        private SpriteRenderer[] _spriteRenderer;
        private BaseBuilding _baseBuilding;

        public bool IsZoneOccupied => _isZoneOccupied;
        public int LeftOrRightSide => _leftOrRightSide;
        public BaseBuilding BaseBuilding => _baseBuilding;

        private void Awake() =>
            _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();

        public void SetZoneOccupeid(bool value)
        {
            _isZoneOccupied = value;

            if (_isZoneOccupied)
                foreach (SpriteRenderer sprite in _spriteRenderer)
                    sprite.gameObject.SetActive(false);

            else if (_isZoneOccupied == false)
                foreach (SpriteRenderer sprite in _spriteRenderer)
                    sprite.gameObject.SetActive(true);
        }

        public void SetBaseBuilding(BaseBuilding baseBuilding)
        {
            _baseBuilding = baseBuilding;
            if (_baseBuilding != null)
                _baseBuilding.SetBuildingArea(this);
        }
    }
}