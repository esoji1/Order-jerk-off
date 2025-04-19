using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.ConstructionBuildings
{
    public class BuildingsBuildView : MonoBehaviour
    {
        [SerializeField] private ActionButton.ActionButton _actionButton;
        [SerializeField] private GameObject _buildingsBuild;
        [SerializeField] private Button _exit;

        private Tween _tween;

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

            _tween = _buildingsBuild.transform
                .DOScale(1.7f, 0.5f);
        }

        private void Hide()
        {
            _tween.Kill();

            _buildingsBuild.SetActive(false);
            _buildingsBuild.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}