using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class SellItemsStore : MonoBehaviour
    {
        [SerializeField] private Button _buy;
        [SerializeField] private List<Cell> _cellList;

        private Player _player;
        private Inventory _inventory;

        private Cell _currentCell;

        public void Initialize(Player player, Inventory inventory)
        {
            _player = player;
            _inventory = inventory;

            foreach (Cell cell in _cellList)
            {
                if (cell.Item.TryGetComponent(out Button button))
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => OnItemClicked(cell));
                }
            }

            _buy.onClick.AddListener(Buy);
        }

        private void OnItemClicked(Cell cell)
        {
            _currentCell = cell;
        }

        private void Buy()
        {
            _player.Wallet.SubtractMoney(_currentCell.Item.Price);
            if (_player.Wallet.Money > 0)
            {
                _inventory.AddItemInCell(_currentCell.Item);
            }
        }

        private void OnDestroy()
        {
            _buy.onClick.RemoveListener(Buy);
        }
    }
}