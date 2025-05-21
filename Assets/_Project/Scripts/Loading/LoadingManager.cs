using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Loading
{
    public class LoadingManager : MonoBehaviour
    {
        public static LoadingManager Instance;

        public GameObject LoadingUI;
        public Slider ProgressBar;
        public TextMeshProUGUI StatusText;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public async Task LoadingSceneAsync(string sceneName, List<LoadingStep> steps)
        {
            LoadingStep loadingSceneStep = new LoadingStep("Загрузка сцены...", async () => await LoadScene(sceneName));
            steps.Add(loadingSceneStep);

            float stepFraction = 1f / steps.Count;
            float totalProgrees = 0f;

            ProgressBar.value = totalProgrees;
            LoadingUI.SetActive(true);

            foreach(LoadingStep step in steps)
            {
                StatusText.text = step.Description;
                await step.ActionAsync();

                totalProgrees += stepFraction;
                ProgressBar.value = totalProgrees;
            }

            LoadingUI.SetActive(false);
        }

        private static async Task LoadScene(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.progress < 0.9f)
                await Task.Yield();

            await Task.Delay(500);

            asyncOperation.allowSceneActivation = true;
        }
    }
}