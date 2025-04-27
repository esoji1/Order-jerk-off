using Assets._Project.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Potions
{
    public class HealingPotions : MonoBehaviour
    {
        [SerializeField] private int _amountHeal;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Button _buttonHeal;
        [SerializeField] private Sctipts.Inventory.Inventory _inventory;
        [SerializeField] private TextMeshProUGUI _textNumberHilokas;

        private bool _isHealingPotions;

        private void Start()
        {
            UpdateNumberHilokas();
        }

        private void OnEnable()
        {
            _buttonHeal.onClick.AddListener(Use);
            _inventory.OnAddItem += UpdateNumberHilokas;
        }

        private void OnDisable()
        {
            _buttonHeal.onClick.RemoveListener(Use);
            _inventory.OnAddItem -= UpdateNumberHilokas;
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
                foreach (Cell cell in _inventory.CellList)
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
            foreach (Cell cell in _inventory.CellList)
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
            for (int i = 0; i < _inventory.CellList.Count; i++)
            {
                if (_inventory.CellList[i].Item == null)
                {
                    _isHealingPotions = false;
                }
                else if (_inventory.CellList[i].Item.GetItemType().Equals(TypesPotion.Hilka))
                {
                    _isHealingPotions = true;
                    break;
                }
            }
        }
    }
}