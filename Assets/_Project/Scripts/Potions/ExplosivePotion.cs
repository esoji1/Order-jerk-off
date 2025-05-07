using _Project.Inventory;
using _Project.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Potions
{
    public class ExplosivePotion : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _radiusAttack;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Button _buttonExplosive;
        [SerializeField] private InventoryActivePotions _inventoryPotions;
        [SerializeField] private TextMeshProUGUI _textNumber;
        [SerializeField] private Explosion _explosion;
        [SerializeField] private ParticleSystem _bom;

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

        private void OnEnable()
        {
            _buttonExplosive.onClick.AddListener(Use);
            _inventoryPotions.OnAddItem += UpdateNumberExplosion;
        }

        private void OnDisable()
        {
            _buttonExplosive.onClick.RemoveListener(Use);
            _inventoryPotions.OnAddItem -= UpdateNumberExplosion;
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
    }
}