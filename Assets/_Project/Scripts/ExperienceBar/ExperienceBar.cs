using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.ExperienceBar
{
    public class ExperienceBar : MonoBehaviour
    {
        [SerializeField] private Slider _experienceBar;
        [SerializeField] private TextMeshProUGUI _experienceBarText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private LevelPlayer _levelPlayer;
        [SerializeField] private Player.Player _player;

        private void Start()
        {
            _experienceBar.maxValue = _levelPlayer.ExperienceReachNewLevel;
            _experienceBar.value = 0;

            UpdateExperience();

            _player.OnAddExperience += UpdateExperience;
            _levelPlayer.OnLevelUp += ResetBar;
        }

        private void UpdateExperience()
        {
            _experienceBarText.text = $"{_levelPlayer.Experience} из {_levelPlayer.ExperienceReachNewLevel}";
            _experienceBar.value = _levelPlayer.Experience;
            _levelText.text = _levelPlayer.Level.ToString();
        }

        private void ResetBar()
        {
            _experienceBar.value = 0;
            _experienceBar.maxValue = _levelPlayer.ExperienceReachNewLevel;
            UpdateExperience();
        }

        private void OnDestroy()
        {
            _player.OnAddExperience -= UpdateExperience;
            _levelPlayer.OnLevelUp -= ResetBar;
        }
    }
}