using _Project.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Potions
{
    public class HealingPotions : MonoBehaviour
    {
        [SerializeField] private int _amountHeal;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Button _buttonHeal;
        [SerializeField] private InventoryActivePotions _inventoryPotions;
        [SerializeField] private TextMeshProUGUI _textNumberHilokas;

        private bool _isHealingPotions;

        private void Start()
        {
            UpdateNumberHilokas();
        }

        private void OnEnable()
        {
            _buttonHeal.onClick.AddListener(Use);
            _inventoryPotions.OnAddItem += UpdateNumberHilokas;
        }

        private void OnDisable()
        {
            _buttonHeal.onClick.RemoveListener(Use);
            _inventoryPotions.OnAddItem -= UpdateNumberHilokas;
        }

        private void Use()
        {
            CheckItemInInventory();

            if (_isHealingPotions == false)
                return;

            int currentHealth = _player.Health.HealthValue;
            int maxHealth = _player.PlayerCharacteristics.Health;
            int amountToHeal = Mathf.Clamp(_amountHeal, 0, maxHealth - currentHealth);

            if (amountToHeal > 0)
            {
                _player.Health.AddHealth(amountToHeal);
                _player.HealthView.AddHealth(amountToHeal);
                foreach (Cell cell in _inventoryPotions.CellList)
                {
                    if (cell.Item != null)
                    {
                        if (cell.Item.GetItemType().Equals(TypesPotion.Hilka))
                        {
                            cell.SubtractNumberItems(1);
                            _textNumberHilokas.text = cell.NumberItems.ToString();
                            if (cell.NumberItems <= 0)
                            {
                                cell.SetIsCellBusy(false);
                                Destroy(cell.Item.gameObject);
                                cell.Item = null;
                            }
                        }
                    }
                }
            }
        }

        private void UpdateNumberHilokas()
        {
            foreach (Cell cell in _inventoryPotions.CellList)
            {
                if (cell.Item != null)
                {
                    if (cell.Item.GetItemType().Equals(TypesPotion.Hilka))
                    {
                        _textNumberHilokas.text = cell.NumberItems.ToString();
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
                    _isHealingPotions = false;
                }
                else if (_inventoryPotions.CellList[i].Item.GetItemType().Equals(TypesPotion.Hilka))
                {
                    _isHealingPotions = true;
                    break;
                }
            }
        }
    }
}