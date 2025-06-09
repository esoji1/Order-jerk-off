using _Project.Artifacts;
using _Project.CameraMain;
using _Project.Core.HealthSystem;
using _Project.ExperienceBar;
using _Project.ImprovingCharacteristicsPlayer;
using _Project.JoystickMovement;
using _Project.ScriptableObjects.Configs;
using UnityEngine;

namespace _Project.Player
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
        [SerializeField] private AdaptingColliderResolution _adaptingColliderResolution;
        [SerializeField] private ChoosingUpgrade _choosingUpgrade;
        [SerializeField] private ManagerAtrefact _managerAtrefact;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private ItemData _itemData;

        private void Awake()
        {
            _player.Initialize(_config, _joysickForMovement, _levelPlayer, _healthInfoPrefab, _healthViewPrefab, _dynamic,
                _useWeapons, _adaptingColliderResolution, _choosingUpgrade, _managerAtrefact, _inventory, _itemData);
        }

        private void Start()
        {
            _player.Wallet.AddMoney(50);
        }
    }
}