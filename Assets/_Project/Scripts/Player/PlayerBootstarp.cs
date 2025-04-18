using Assets._Project.Scripts.ExperienceBar;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Sctipts.Core.HealthSystem;
using Assets._Project.Sctipts.JoystickMovement;
using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    public class PlayerBootstarp : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private JoysickForMovement _joysickForMovement;
        [SerializeField] private LevelPlayer _levelPlayer;
        [SerializeField] private HealthInfo _healthInfoPrefab;
        [SerializeField] private HealthView _healthViewPrefab;
        [SerializeField] private Canvas _dynamic;
        [SerializeField] private UseWeapons.UseWeapons _useWeapons;

        private void Awake()
        {
            _player.Initialize(_config, _joysickForMovement, _levelPlayer, _healthInfoPrefab, _healthViewPrefab, _dynamic, _useWeapons);
        }

        private void Start()
        {
            _player.Wallet.AddMoney(50);
        }
    }
}