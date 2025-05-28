using _Project.Inventory;
using _Project.Potions.Projectile;
using _Project.Weapon;
using TMPro;
using UnityEngine;

namespace _Project.Potions
{
    public class MolotovCocktailPotion : BasePotion
    {
        private Molotov _molotov;
        private DetectionRadius _enemyDetectionRadius;
        private Collider2D _nearestEnemy; 
        private IncendiaryZone _incendiaryZonePrefab;
        private TextMeshProUGUI _textDamage;
        private Canvas _dynamic;

        public void Initialize(Player.Player player, InventoryActivePotions inventoryPotions, Molotov molotov, ManagerPotion managerPotion, IncendiaryZone incendiaryZonePrefab,
             TextMeshProUGUI textDamage, Canvas dynamic)
        {
            base.Initialize(player, inventoryPotions, managerPotion);
            _molotov = molotov;
            _incendiaryZonePrefab = incendiaryZonePrefab;
            _textDamage = textDamage;
            _dynamic = dynamic;
            PotionType = TypesPotion.MolotovCocktail;
        }

        private void Awake()
        {
            PotionType = TypesPotion.MolotovCocktail;
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
                GameObject molotov = Instantiate(_molotov.gameObject, Player.transform.position, Quaternion.identity, null);
                Molotov molotov1 = molotov.GetComponent<Molotov>();
                molotov1.Initialize(Player, direction, (int)EffectValue, (int)SecondaryValue, _incendiaryZonePrefab, _textDamage, _dynamic);
                return true;
            }

            return false;
        }
    }
}
