using System.Text;
using TMPro;
using UnityEngine;

namespace _Project.Craft
{
    public class ItemTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCraft;
        [SerializeField] private TextMeshProUGUI _textDescriptionItem;

        private InventoryCrafting _inventoryCrafting;

        public void Initialize(InventoryCrafting inventoryCrafting)
        {
            _inventoryCrafting = inventoryCrafting;

            _inventoryCrafting.OnClickCraft += UpdateText;
        }

        private void UpdateText(Craft craft)
        {
            StringBuilder textCtaft = new StringBuilder();

            foreach (QuantityItemCraft item in craft.List)
                textCtaft.Append($"{item.Item.Name}({item.Quantity} шт)\n");

            _textCraft.text = textCtaft.ToString();
            _textDescriptionItem.text = craft.CraftItem.Description;
        }

        private void OnDestroy() => _inventoryCrafting.OnClickCraft -= UpdateText;
    }
}