using _Project.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Potions
{
    public class HealingPotions : BasePotion
    {
        //[SerializeField] private int _amountHeal;

        //private Player.Player Player;
        //private InventoryActivePotions InventoryPotions;

        //private TextMeshProUGUI TextNumber;
        //private Button _buttonHeal;

        //private bool _isHealingPotions;

        //private void Start()
        //{
        //    UpdateNumberHilokas();
        //}

        //public void Initialize(Player.Player player, InventoryActivePotions inventoryPotions)
        //{
        //    ExtractComponents();

        //    Player = player;
        //    InventoryPotions = inventoryPotions;

        //    _buttonHeal.onClick.AddListener(Use);
        //    InventoryPotions.OnAddItem += UpdateNumberHilokas;
        //}

        //private void ExtractComponents()
        //{
        //    TextNumber = GetComponentInChildren<TextMeshProUGUI>();
        //    _buttonHeal = GetComponent<Button>();
        //}

        //private void Use()
        //{
        //    CheckItemInInventory();

        //    if (_isHealingPotions == false)
        //        return;

        //    int currentHealth = Player.Health.HealthValue;
        //    int maxHealth = Player.PlayerCharacteristics.Health;
        //    int amountToHeal = Mathf.Clamp(_amountHeal, 0, maxHealth - currentHealth);

        //    if (amountToHeal > 0)
        //    {
        //        Player.Health.AddHealth(amountToHeal);
        //        Player.HealthView.AddHealth(amountToHeal);
        //        foreach (Cell cell in InventoryPotions.CellList)
        //        {
        //            if (cell.Item != null)
        //            {
        //                if (cell.Item.GetItemType().Equals(TypesPotion.Hilka))
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

        //private void UpdateNumberHilokas()
        //{
        //    foreach (Cell cell in InventoryPotions.CellList)
        //    {
        //        if (cell.Item != null)
        //        {
        //            if (cell.Item.GetItemType().Equals(TypesPotion.Hilka))
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
        //            _isHealingPotions = false;
        //        }
        //        else if (InventoryPotions.CellList[i].Item.GetItemType().Equals(TypesPotion.Hilka))
        //        {
        //            _isHealingPotions = true;
        //            break;
        //        }
        //    }
        //}

        //private void OnDestroy()
        //{
        //    _buttonHeal.onClick.RemoveListener(Use);
        //    InventoryPotions.OnAddItem -= UpdateNumberHilokas;
        //}
        private void Awake()
        {
            PotionType = TypesPotion.Hilka;
        }

        protected override bool ApplyEffect()
        {
            int currentHealth = Player.Health.HealthValue;
            int maxHealth = Player.PlayerCharacteristics.Health;
            int amountToHeal = Mathf.Clamp(_effectValue, 0, maxHealth - currentHealth);

            if (amountToHeal > 0)
            {
                Player.Health.AddHealth(amountToHeal);
                Player.HealthView.AddHealth(amountToHeal);
                return true;
            }

            return false;
        }
    }
}