using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Core
{
    public class MenuButtonManager : MonoBehaviour
    {
        [SerializeField] private Button _play;
        [SerializeField] private Button _setting;
        [SerializeField] private Button _exit;

        private void OnEnable()
        {
            _play.onClick.AddListener(OpenGameplayScene);
            _setting.onClick.AddListener(OpenSettingWindow);
            _exit.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            _play.onClick.RemoveListener(OpenGameplayScene);
            _setting.onClick.RemoveListener(OpenSettingWindow);
            _exit.onClick.RemoveListener(Exit);
        }

        private void OpenGameplayScene() =>
            SceneManager.LoadScene((int)TypesScene.GameplayScene);

        private void OpenSettingWindow() { }

        private void Exit() => Application.Quit();
    }
}