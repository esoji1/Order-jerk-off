using _Project.Inventory;
using _Project.Weapon;
using UnityEngine;

namespace _Project.Potions
{
    public class ExplosivePotion : BasePotion
    {
        private ParticleSystem _bom;
        private Explosion _explosion;
        private DetectionRadius _enemyDetectionRadius;
        private Collider2D _nearestEnemy;

        public void Initialize(Player.Player player, InventoryActivePotions inventoryPotions,
            ParticleSystem bom, Explosion explosion, ManagerPotion managerPotion)
        {
            base.Initialize(player, inventoryPotions, managerPotion);
            _bom = bom;
            _explosion = explosion;
            PotionType = TypesPotion.Explosion;
        }

        private void Awake()
        {
            PotionType = TypesPotion.Explosion;
        }

        private void Update()
        {
            if (_enemyDetectionRadius == null && Player != null)
            {
                _enemyDetectionRadius = new DetectionRadius(Player.transform, Player.Config.LayerEnemy);
            }

            if (_enemyDetectionRadius != null)
            {
                _enemyDetectionRadius.Detection(Player.Config.VisibilityRadius);
                _nearestEnemy = _enemyDetectionRadius.GetNearestEnemy();
            }
        }

        protected override bool ApplyEffect()
        {
            if (_nearestEnemy != null)
            {
                Vector2 direction = (_nearestEnemy.transform.position - Player.transform.position).normalized;
                GameObject explosion = Instantiate(_explosion.gameObject, Player.transform.position, Quaternion.identity, null);
                Explosion explosion1 = explosion.GetComponent<Explosion>();
                explosion1.Initialize(Player, direction, (int)EffectValue, (int)SecondaryValue, _bom);
                return true;
            }

            return false;
        }
    }
}