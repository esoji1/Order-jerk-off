using System;
using TMPro;
using UnityEngine;

namespace _Project.ExperienceBar
{
    public class LevelPlayer : MonoBehaviour
    {
        private const float ExperienceMultiplier = 1.2f;

        private int _experience;
        private int _experienceReachNewLevel;
        private int _level;

        public event Action OnLevelUp;

        public int ExperienceReachNewLevel => _experienceReachNewLevel;
        public int Experience => _experience;
        public int Level => _level;

        public bool Up;
        public bool Down;

        private void Awake()
        {
            _level = 1;
            _experience = 0;
            _experienceReachNewLevel = 100;
        }

        private void Update()
        {
            if (Up)
            {
                AddExperience(50);
                Up = false;
            }
            else if (Down)
            {
                SubtractExperience(50);
                Down = false;
            }
        }

        public void AddExperience(int value)
        {
            _experience += value;
            CheckIncreaseDecreaseLevel();
        }

        public void SubtractExperience(int value)
        {
            _experience -= value;
            CheckIncreaseDecreaseLevel();
        }

        public void CheckIncreaseDecreaseLevel()
        {
            while (_experience >= _experienceReachNewLevel)
            {
                _experience -= _experienceReachNewLevel;
                _level++;
                _experienceReachNewLevel = Mathf.RoundToInt(_experienceReachNewLevel * ExperienceMultiplier);
                OnLevelUp?.Invoke();
            }

            while (_level > 1 && _experience < 0)
            {
                _level--;
                _experience += Mathf.RoundToInt(_experienceReachNewLevel / ExperienceMultiplier);
                _experienceReachNewLevel = Mathf.RoundToInt(_experienceReachNewLevel / ExperienceMultiplier);
            }

            if (_level == 1 && _experience < 0)
            {
                _level = 1;
                _experience = 0;
                _experienceReachNewLevel = 100;
            }
        }
    }
}