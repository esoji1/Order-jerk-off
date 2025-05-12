using _Project.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Potions
{
    public abstract class BasePotion : MonoBehaviour
    {
        [SerializeField] protected int EffectValue;
        [SerializeField] protected int SecondaryValue; 

        protected Player.Player Player;
        protected InventoryActivePotions InventoryPotions;
        protected ManagerPotion ManagerPotion;

        protected TextMeshProUGUI TextNumber;
        protected Button PotionButton;

        protected bool HasPotions;
        protected TypesPotion PotionType;

        protected virtual void Start()
        {
            UpdateNumberPotions();
        }

        public virtual void Initialize(Player.Player player, InventoryActivePotions inventoryPotions, ManagerPotion managerPotion)
        {
            ExtractComponents();

            Player = player;
            InventoryPotions = inventoryPotions;
            ManagerPotion = managerPotion;

            PotionButton.onClick.AddListener(Use);
            InventoryPotions.OnAddItem += UpdateNumberPotions;
        }

        protected virtual void ExtractComponents()
        {
            TextNumber = GetComponentInChildren<TextMeshProUGUI>();
            PotionButton = GetComponent<Button>();
        }

        protected virtual void Use()
        {
            CheckItemInInventory();

            if (HasPotions == false) 
                return;

            if (ApplyEffect())
                ConsumePotion();
        }

        protected abstract bool ApplyEffect();

        protected virtual void ConsumePotion()
        {
            foreach (Cell cell in InventoryPotions.CellList)
            {
                if (cell.Item != null && cell.Item.GetItemType().Equals(PotionType))
                {
                    cell.SubtractNumberItems(1);
                    ManagerPotion.SubtractNumberItems(PotionType);
                    TextNumber.text = cell.NumberItems.ToString();

                    if (cell.NumberItems <= 0)
                    {
                        cell.SetIsCellBusy(false);
                        Destroy(cell.Item.gameObject);
                        cell.Item = null;
                        Destroy(gameObject);
                    }
                    break;
                }
            }
        }

        protected virtual void UpdateNumberPotions()
        {
            foreach (Cell cell in InventoryPotions.CellList)
            {
                if (cell.Item != null && cell.Item.GetItemType().Equals(PotionType))
                {
                    TextNumber.text = cell.NumberItems.ToString();
                    break;
                }
            }
        }

        protected virtual void CheckItemInInventory()
        {
            HasPotions = false;
            foreach (Cell cell in InventoryPotions.CellList)
            {
                if (cell.Item != null && cell.Item.GetItemType().Equals(PotionType))
                {
                    HasPotions = true;
                    break;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            PotionButton.onClick.RemoveListener(Use);
            InventoryPotions.OnAddItem -= UpdateNumberPotions;
        }
    }
}