using _Project.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Potions
{
    public class ManagerPotion : MonoBehaviour
    {
        [SerializeField] private InventoryActivePotions _inventoryActivePotions;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private BootstrapFactoryPotion _bootstrapFactoryPotion;

        private ChangeItem _changeItem;

        private Dictionary<Enum, BasePotion> _potionObjects = new Dictionary<Enum, BasePotion>();
        private Dictionary<Enum, int> _potion = new Dictionary<Enum, int>();

        public void Initialize(ChangeItem changeItem)
        {
            _changeItem = changeItem;

            _changeItem.OnAddPotion += AddPotion;
            _changeItem.OnSubstractPotion += SubstractPotion;
        }

        public void SubtractNumberItems(TypesPotion type)
        {
            IdentifyPotionSubstract(type);
        }

        private void OnDestroy()
        {
            _changeItem.OnAddPotion -= AddPotion;
            _changeItem.OnSubstractPotion -= SubstractPotion;
        }

        private void AddPotion(Enum type)
        {
            CheckItemInInventory();

            foreach (Cell cell in _inventory.CellList)
            {
                if (cell.Item != null)
                {
                    IdentifyPotionAdd(type);
                    break;
                }
            }
        }

        private void SubstractPotion(Enum type)
        {
            for (int i = 0; i < _inventoryActivePotions.CellList.Count; i++)
            {
                if (_inventoryActivePotions.CellList[i].Item != null)
                {
                    IdentifyPotionSubstract(type);
                    break;
                }
            }
        }

        private void IdentifyPotionAdd(Enum type)
        {
            if (_potionObjects.ContainsKey(type))
            {
                Debug.Log("Снова");
                _potion[type]++;
            }
            else
            {
                BasePotion potion = _bootstrapFactoryPotion.Factory.Get((TypesPotion)type);
                if (potion != null)
                {
                    _potionObjects[type] = potion;
                    _potion[type] = 1;
                }
            }
        }

        private void IdentifyPotionSubstract(Enum type)
        {
            if (_potion.ContainsKey(type))
            {
                _potion[type]--;

                if (_potion[type] <= 0)
                {
                    Destroy(_potionObjects[type].gameObject);
                    _potionObjects.Remove(type);
                    _potion.Remove(type);
                }
            }
        }

        private bool CheckItemInInventory()
        {
            for (int i = 0; i < _inventory.CellList.Count; i++)
            {
                if (_inventory.CellList[i].Item != null)
                {
                    if (_inventory.CellList[i].Item.Category == ItemCategory.Potions)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}