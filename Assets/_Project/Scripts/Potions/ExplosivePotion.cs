using _Project.Inventory;
using _Project.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Potions
{
    public class ExplosivePotion : BasePotion
    {
        //[SerializeField] private int _damage;
        //[SerializeField] private int _radiusAttack;

        //private InventoryActivePotions InventoryPotions;
        //private Player.Player Player;
        //private ParticleSystem _bom;
        //private Explosion _explosion;

        //private TextMeshProUGUI TextNumber;
        //private Button _buttonExplosive;

        //private bool _isExplosivePotions;
        //private DetectionRadius _enemyDetectionRadius;
        //private Collider2D _nearestEnemy;

        //private void Start()
        //{
        //    _enemyDetectionRadius = new DetectionRadius(Player.transform, Player.Config.LayerEnemy);

        //    UpdateNumberExplosion();
        //}

        //private void Update()
        //{
        //    _enemyDetectionRadius.Detection(Player.Config.VisibilityRadius);
        //    _nearestEnemy = _enemyDetectionRadius.GetNearestEnemy();
        //}

        //public void Initialize(Player.Player player, InventoryActivePotions inventoryPotions, ParticleSystem bom, Explosion explosion)
        //{
        //    ExtractComponents();

        //    Player = player;
        //    InventoryPotions = inventoryPotions;
        //    _bom = bom;
        //    _explosion = explosion;

        //    _buttonExplosive.onClick.AddListener(Use);
        //    InventoryPotions.OnAddItem += UpdateNumberExplosion;
        //}

        //private void ExtractComponents()
        //{
        //    TextNumber = GetComponentInChildren<TextMeshProUGUI>();
        //    _buttonExplosive = GetComponent<Button>();
        //}

        //private void Use()
        //{
        //    CheckItemInInventory();

        //    if (_isExplosivePotions == false)
        //        return;

        //    foreach (Cell cell in InventoryPotions.CellList)
        //    {
        //        if (cell.Item != null)
        //        {
        //            if (cell.Item.GetItemType().Equals(TypesPotion.Explosion))
        //            {
        //                if (Attack())
        //                {
        //                    cell.SubtractNumberItems(1);
        //                    TextNumber.text = cell.NumberItems.ToString();
        //                    if (cell.NumberItems <= 0)
        //                    {
        //                        cell.SetIsCellBusy(false);
        //                        Destroy(cell.Item.gameObject);
        //                        cell.Item = null;
        //                        Destroy(gameObject);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private void UpdateNumberExplosion()
        //{
        //    foreach (Cell cell in InventoryPotions.CellList)
        //    {
        //        if (cell.Item != null)
        //        {
        //            if (cell.Item.GetItemType().Equals(TypesPotion.Explosion))
        //            {
        //                TextNumber.text = cell.NumberItems.ToString();
        //            }
        //        }
        //    }
        //}

        //private void CheckItemInInventory()
        //{
        //    for (int i = 0; i < InventoryPotions.CellList.Count; i++)
        //    {
        //        if (InventoryPotions.CellList[i].Item == null)
        //        {
        //            _isExplosivePotions = false;
        //        }
        //        else if (InventoryPotions.CellList[i].Item.GetItemType().Equals(TypesPotion.Explosion))
        //        {
        //            _isExplosivePotions = true;
        //            break;
        //        }
        //    }
        //}

        //private bool Attack()
        //{
        //    if (_nearestEnemy != null)
        //    {
        //        Vector2 direction = (_nearestEnemy.transform.position - Player.transform.position).normalized;
        //        GameObject explosion = Instantiate(_explosion.gameObject, Player.transform.position, Quaternion.identity, null);
        //        Explosion explosion1 = explosion.GetComponent<Explosion>();
        //        explosion1.Initialize(Player, direction, _damage, _radiusAttack, _bom);
        //        return true;
        //    }

        //    return false;   
        //}

        //private void OnDestroy()
        //{
        //    _buttonExplosive.onClick.RemoveListener(Use);
        //    InventoryPotions.OnAddItem -= UpdateNumberExplosion;
        //}
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
                explosion1.Initialize(Player, direction, _effectValue, _secondaryValue, _bom);
                return true;
            }

            return false;
        }
    }
}