using _Project.Loading;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.ScriptsLoading
{
    public class EntryPoint : MonoBehaviour
    {
        private readonly List<string> _phrases = new()
        {
            "Меч ржавый, но цель благородная",
            "Меч ржавый, но цель благородная",
            "Подземелья не прощают ошибок",
            "Золото? Уже потрачено на зелья",
            "Крит — это не удача, это судьба",
            "Драконов много, а я один",
            "Ловушки — лучшие учителя",
            "Босс? Значит, будет лут",
            "Спасбросок или смерть",
            "РПГ — это когда кости решают всё"
        };

        private async void Start()
        {
            List<LoadingStep> loadingSteps = new List<LoadingStep>()
            {
                new LoadingStep(GetRandomPhrase(), () => Task.Delay(1500)),
                new LoadingStep(GetRandomPhrase(), () => Task.Delay(1500)),
                new LoadingStep(GetRandomPhrase(), () => Task.Delay(1500)),
                new LoadingStep(GetRandomPhrase(), () => Task.Delay(1500)),
            };

            await LoadingManager.Instance.LoadingSceneAsync("GemplayScene", loadingSteps);
        }

        private string GetRandomPhrase()
        {
            int rIndex = Random.Range(0, _phrases.Count);
            string rPhrase = _phrases[rIndex];

            return rPhrase;
        }
    }
}