using Assets._Project.Scripts.ConstructionBuildings;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.ActionButton
{
    public class ActionButton : MonoBehaviour
    {
        private const string BuildingAreaLayer = "BuildingArea";

        [SerializeField] private Button _actionButton;

        private Collider2D _collision2D;

        public event Action<BuildingArea> OnStandingInConstructionZone;

        private void OnEnable()
        {
            _actionButton.onClick.AddListener(PerformAction);
        }

        private void OnDisable()
        {
            _actionButton.onClick.RemoveListener(PerformAction);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _collision2D = collision;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _collision2D = null;
        }

        private void PerformAction()
        {
            if (_collision2D == null)
                return;

            if (_collision2D.gameObject.layer == LayerMask.NameToLayer(BuildingAreaLayer) && _collision2D.TryGetComponent(out BuildingArea buildingArea))
            {
                if (buildingArea.IsZoneOccupied == false)
                {
                    OnStandingInConstructionZone?.Invoke(buildingArea);
                    return;
                }
                else if (buildingArea.IsZoneOccupied)
                {
                    buildingArea.BaseBuilding.Show();
                }
            }
        }
    }
}