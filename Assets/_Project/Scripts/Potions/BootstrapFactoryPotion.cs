using _Project.Core.Projectile;
using _Project.Inventory;
using _Project.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace _Project.Potions
{
    public class BootstrapFactoryPotion : MonoBehaviour
    {
        [SerializeField] private PotionConfig _explosionConfig, _healingConfig, _speedConfig, _molotovCocktailConfig, _invisibilityConfig, _sleepingConfig,
            dealPoisonConfig;
        [SerializeField] private Player.Player _player;
        [SerializeField] private InventoryActivePotions inventoryActivePotions;
        [SerializeField] private Transform _content;
        [SerializeField] private ParticleSystem _bom;
        [SerializeField] private Explosion _explosion;
        [SerializeField] private ManagerPotion _managerPotion; 
        [SerializeField] private Molotov _molotov;
        [SerializeField] private IncendiaryZone _incendiaryZonePrefab;
        [SerializeField] private TextMeshProUGUI _textDamage;
        [SerializeField] private Canvas _dynamic;

        private PotionFactory _factory;

        public PotionFactory Factory => _factory;

        private void Awake()
        {
            _factory = new PotionFactory(_explosionConfig, _healingConfig, _speedConfig, _molotovCocktailConfig, _invisibilityConfig, _sleepingConfig,
                dealPoisonConfig, _player, inventoryActivePotions, _content, _bom, _explosion, _managerPotion, _molotov, _incendiaryZonePrefab, _textDamage, _dynamic);
        }
    }
}