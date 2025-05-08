using _Project.Inventory;
using _Project.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Potions
{
    public class ExplosivePotion : BasePotion
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _radiusAttack;

        private InventoryActivePotions _inventoryPotions;
        private Player.Player _player;
        private ParticleSystem _bom;
        private Explosion _explosion;

        private TextMeshProUGUI _textNumber;
        private Button _buttonExplosive;

        private bool _isExplosivePotions;
        private DetectionRadius _enemyDetectionRadius;
        private Collider2D _nearestEnemy;

        private void Start()
        {
            _enemyDetectionRadius = new DetectionRadius(_player.transform, _player.Config.LayerEnemy);

            UpdateNumberExplosion();
        }

        private void Update()
        {
            _enemyDetectionRadius.Detection(_player.Config.VisibilityRadius);
            _nearestEnemy = _enemyDetectionRadius.GetNearestEnemy();
        }

        public void Initialize(Player.Player player, InventoryActivePotions inventoryPotions, ParticleSystem bom, Explosion explosion)
        {
            ExtractComponents();

            _player = player;
            _inventoryPotions = inventoryPotions;
            _bom = bom;
            _explosion = explosion;

            _buttonExplosive.onClick.AddListener(Use);
            _inventoryPotions.OnAddItem += UpdateNumberExplosion;
        }

        private void ExtractComponents()
        {
            _textNumber = GetComponentInChildren<TextMeshProUGUI>();
            _buttonExplosive = GetComponent<Button>();
        }

        private void Use()
        {
            //CheckItemInInventory();

            //if (_isExplosivePotions == false)
            //return;

            //foreach (Cell cell in _inventoryPotions.CellList)
            //{
            //    if (cell.Item != null)
            //    {
            //        if (cell.Item.GetItemType().Equals(TypesPotion.Explosion))
            //        {
            Attack();
            //            cell.SubtractNumberItems(1);
            //            _textNumber.text = cell.NumberItems.ToString();
            //            if (cell.NumberItems <= 0)
            //            {
            //                cell.SetIsCellBusy(false);
            //                Destroy(cell.Item.gameObject);
            //                cell.Item = null;
            //            }
            //        }
            //    }
            //}
        }

        private void UpdateNumberExplosion()
        {
            foreach (Cell cell in _inventoryPotions.CellList)
            {
                if (cell.Item != null)
                {
                    if (cell.Item.GetItemType().Equals(TypesPotion.Explosion))
                    {
                        _textNumber.text = cell.NumberItems.ToString();
                    }
                }
            }
        }

        private void CheckItemInInventory()
        {
            for (int i = 0; i < _inventoryPotions.CellList.Count; i++)
            {
                if (_inventoryPotions.CellList[i].Item == null)
                {
                    _isExplosivePotions = false;
                }
                else if (_inventoryPotions.CellList[i].Item.GetItemType().Equals(TypesPotion.Explosion))
                {
                    _isExplosivePotions = true;
                    break;
                }
            }
        }

        private void Attack()
        {
            if (_nearestEnemy != null)
            {
                Vector2 direction = (_nearestEnemy.transform.position - _player.transform.position).normalized;
                GameObject explosion = Instantiate(_explosion.gameObject, _player.transform.position, Quaternion.identity, null);
                Explosion explosion1 = explosion.GetComponent<Explosion>();
                explosion1.Initialize(_player, direction, _damage, _radiusAttack, _bom);
            }
        }

        private void OnDestroy()
        {
            _buttonExplosive.onClick.RemoveListener(Use);
            _inventoryPotions.OnAddItem -= UpdateNumberExplosion;
        }
    }
}