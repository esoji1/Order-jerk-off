using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class BuildingsBuildView : MonoBehaviour
    {
        [SerializeField] private ActionButton.ActionButton _actionButton;
        [SerializeField] private GameObject _buildingsBuild;
        [SerializeField] private Button _exit;

        private void OnEnable()
        {
            _actionButton.OnStandingInConstructionZone += Show;
            _exit.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _actionButton.OnStandingInConstructionZone -= Show;
            _exit.onClick.RemoveListener(Hide);
        }

        private void Show(BuildingArea buildingArea)
        {
            _buildingsBuild.SetActive(true);
        }

        private void Hide()
        {
            _buildingsBuild.SetActive(false);
        }
    }
}