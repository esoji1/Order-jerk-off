using _Project.Inventory;
using _Project.ScriptableObjects;
using UnityEngine;

namespace _Project.Potions
{
    public class BootstrapFactoryPotion : MonoBehaviour
    {
        [SerializeField] private PotionConfig _explosionConfig, _healingConfig;
        [SerializeField] private Player.Player _player;
        [SerializeField] private InventoryActivePotions inventoryActivePotions;
        [SerializeField] private Transform _content;
        [SerializeField] private ParticleSystem _bom;
        [SerializeField] private Explosion _explosion;
        [SerializeField] private ManagerPotion _managerPotion;

        private PotionFactory _factory;

        public PotionFactory Factory => _factory;

        private void Awake()
        {
            _factory = new PotionFactory(_explosionConfig, _healingConfig, _player, inventoryActivePotions, _content, _bom, _explosion, _managerPotion);
        }
    }
}