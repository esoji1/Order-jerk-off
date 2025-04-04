using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class BuildingArea : MonoBehaviour
    {
        [SerializeField] private int _leftOrRightSide;

        private bool _isZoneOccupied;
        private SpriteRenderer _spriteRenderer;

        public bool IsZoneOccupied => _isZoneOccupied;
        public int LeftOrRightSide => _leftOrRightSide;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetZoneOccupeid(bool value)
        {
            _isZoneOccupied = value;

            if (_isZoneOccupied)
            {
                _spriteRenderer.gameObject.SetActive(false);
            }
            else if (_isZoneOccupied == false)
            {
                _spriteRenderer.gameObject.SetActive(true);
            }
        }
    }
}