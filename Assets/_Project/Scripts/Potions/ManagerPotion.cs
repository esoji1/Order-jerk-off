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

        private Dictionary<TypesPotion, BasePotion> _potionObjects = new Dictionary<TypesPotion, BasePotion>();
        private Dictionary<TypesPotion, int> _potion = new Dictionary<TypesPotion, int>();

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
            if (type is TypesPotion specificType)
            {
                switch (specificType)
                {
                    case TypesPotion.Hilka:
                        if (_potionObjects.ContainsKey(TypesPotion.Hilka))
                        {
                            Debug.Log("Снова");
                            _potion[TypesPotion.Hilka]++;
                        }
                        else
                        {
                            BasePotion potion = _bootstrapFactoryPotion.Factory.Get(TypesPotion.Hilka);
                            _potionObjects[TypesPotion.Hilka] = potion;
                            _potion[TypesPotion.Hilka] = 1;
                        }
                        break;

                    case TypesPotion.Explosion:
                        if (_potionObjects.ContainsKey(TypesPotion.Explosion))
                        {
                            Debug.Log("Снова");
                            _potion[TypesPotion.Explosion]++;
                        }
                        else
                        {
                            BasePotion potion = _bootstrapFactoryPotion.Factory.Get(TypesPotion.Explosion);
                            _potionObjects[TypesPotion.Explosion] = potion;
                            _potion[TypesPotion.Explosion] = 1;
                        }
                        break;

                    case TypesPotion.SpeedUp:
                        if (_potionObjects.ContainsKey(TypesPotion.SpeedUp))
                        {
                            Debug.Log("Снова");
                            _potion[TypesPotion.SpeedUp]++;
                        }
                        else
                        {
                            BasePotion potion = _bootstrapFactoryPotion.Factory.Get(TypesPotion.SpeedUp);
                            _potionObjects[TypesPotion.SpeedUp] = potion;
                            _potion[TypesPotion.SpeedUp] = 1;
                        }
                        break;

                    case TypesPotion.MolotovCocktail:
                        if (_potionObjects.ContainsKey(TypesPotion.MolotovCocktail))
                        {
                            Debug.Log("Снова");
                            _potion[TypesPotion.MolotovCocktail]++;
                        }
                        else
                        {
                            BasePotion potion = _bootstrapFactoryPotion.Factory.Get(TypesPotion.MolotovCocktail);
                            _potionObjects[TypesPotion.MolotovCocktail] = potion;
                            _potion[TypesPotion.MolotovCocktail] = 1;
                        }
                        break;
                }
            }
        }

        private void IdentifyPotionSubstract(Enum type)
        {
            if (type is TypesPotion specificType)
            {
                switch (specificType)
                {
                    case TypesPotion.Hilka:
                        _potion[TypesPotion.Hilka]--;

                        if (_potion[TypesPotion.Hilka] <= 0)
                        {
                            Destroy(_potionObjects[TypesPotion.Hilka].gameObject);
                            _potionObjects.Remove(TypesPotion.Hilka);
                        }
                        break;

                    case TypesPotion.Explosion:
                        _potion[TypesPotion.Explosion]--;

                        if (_potion[TypesPotion.Explosion] <= 0)
                        {
                            Destroy(_potionObjects[TypesPotion.Explosion].gameObject);
                            _potionObjects.Remove(TypesPotion.Explosion);
                        }
                        break;

                    case TypesPotion.SpeedUp:
                        _potion[TypesPotion.SpeedUp]--;

                        if (_potion[TypesPotion.SpeedUp] <= 0)
                        {
                            Destroy(_potionObjects[TypesPotion.SpeedUp].gameObject);
                            _potionObjects.Remove(TypesPotion.SpeedUp);
                        }
                        break;

                    case TypesPotion.MolotovCocktail:
                        _potion[TypesPotion.MolotovCocktail]--;

                        if (_potion[TypesPotion.MolotovCocktail] <= 0)
                        {
                            Destroy(_potionObjects[TypesPotion.MolotovCocktail].gameObject);
                            _potionObjects.Remove(TypesPotion.MolotovCocktail);
                        }
                        break;
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