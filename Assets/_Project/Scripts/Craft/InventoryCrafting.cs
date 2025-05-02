using Assets._Project.Scripts.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Craft
{
    public class InventoryCrafting : MonoBehaviour
    {
        private Sctipts.Inventory.Inventory _inventory;
        private Dictionary<Cell, Sctipts.Craft.Craft> _cellAndCraftDictionary;
        private Dictionary<Button, UnityAction> _buttonSubscriptions = new Dictionary<Button, UnityAction>();

        public Sctipts.Inventory.Inventory Inventory => _inventory;

        public event Action<Sctipts.Craft.Craft> OnClickCraft;

        public void Initialize(Sctipts.Inventory.Inventory inventory)
        {
            UnsubscribeAll();

            _inventory = inventory;
            _cellAndCraftDictionary = new Dictionary<Cell, Sctipts.Craft.Craft>();

            foreach (Cell cell in GetComponentsInChildren<Cell>())
            {
                Sctipts.Craft.Craft craftComponent = cell.GetComponent<Sctipts.Craft.Craft>();
                _cellAndCraftDictionary.Add(cell, craftComponent);

                if (cell.Item != null && cell.Item.TryGetComponent(out Button button))
                {
                    UnityAction onClickAction = () => OnClick(craftComponent);
                    button.onClick.AddListener(onClickAction);

                    _buttonSubscriptions[button] = onClickAction;
                }
            }
        }

        private void OnClick(Sctipts.Craft.Craft craft) => OnClickCraft?.Invoke(craft);

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