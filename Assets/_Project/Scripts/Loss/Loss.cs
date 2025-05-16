using _Project.ConstructionBuildings.Buildings;
using _Project.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Loss
{
    public class Loss : MonoBehaviour
    {
        [SerializeField] private GameObject _windowLoss;
        [SerializeField] private Button _exitHome;
        [SerializeField] private Button _restart;

        private BaseBuilding _baseBuilding;

        private Tween _tween;

        public void Initialize(BaseBuilding baseBuilding)
        {
            _baseBuilding = baseBuilding;

            _baseBuilding.OnDestruction += PerformActionsLoss;
            _exitHome.onClick.AddListener(ExitHome);
            _restart.onClick.AddListener(Restart);
        }

        private void PerformActionsLoss()
        {
            ShowWindowLoss();
            Time.timeScale = 0f;
        }

        private void ShowWindowLoss()
        {
            _windowLoss.SetActive(true);
            _tween = _windowLoss.transform
                .DOScale(1, 0.5f)
                .SetUpdate(true);
        }

        private void Restart()
        {
            _tween.Kill();
            Time.timeScale = 1f;
            SceneManager.LoadScene((int)TypesScene.GameplayScene);
        }

        private void ExitHome()
        {
            _tween.Kill();
            Time.timeScale = 1f;
            SceneManager.LoadScene((int)TypesScene.MenuScene);
        }

        private void OnDestroy()
        {
            _baseBuilding.OnDestruction -= PerformActionsLoss;
            _exitHome.onClick.RemoveListener(ExitHome);
            _restart.onClick.RemoveListener(Restart);
        }
    }
}