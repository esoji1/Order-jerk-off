using _Project.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Craft
{
    public class InventoryCrafting : MonoBehaviour
    {
        private Inventory.Inventory _inventory;
        private Dictionary<Cell, Craft> _cellAndCraftDictionary;
        private Dictionary<Button, UnityAction> _buttonSubscriptions = new Dictionary<Button, UnityAction>();

        public Inventory.Inventory Inventory => _inventory;

        public event Action<Craft> OnClickCraft;

        public void Initialize(Inventory.Inventory inventory)
        {
            UnsubscribeAll();

            _inventory = inventory;
            _cellAndCraftDictionary = new Dictionary<Cell, Craft>();

            foreach (Cell cell in GetComponentsInChildren<Cell>())
            {
                Craft craftComponent = cell.GetComponent<Craft>();
                _cellAndCraftDictionary.Add(cell, craftComponent);

                if (cell.Item != null && cell.Item.TryGetComponent(out Button button))
                {
                    UnityAction onClickAction = () => OnClick(craftComponent);
                    button.onClick.AddListener(onClickAction);

                    _buttonSubscriptions[button] = onClickAction;
                }
            }
        }

        private void OnClick(Craft craft) => OnClickCraft?.Invoke(craft);

        private void UnsubscribeAll()
        {
            foreach (KeyValuePair<Button, UnityAction> pair in _buttonSubscriptions)
                if (pair.Key != null)
                    pair.Key.onClick.RemoveListener(pair.Value);

            _buttonSubscriptions.Clear();
        }

        private void OnDestroy() => UnsubscribeAll();
    }
}