using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class SaleItem : MonoBehaviour
    {
        [SerializeField] private Button _sell;

        private Inventory _inventory;
        private Player _player;

        private Cell _currentCell;

        public void Initialize(Player player, Inventory inventor)
        {
            _player = player;
            _inventory = inventor;

            _sell.onClick.AddListener(Sell);
            _inventory.OnClickedItem += ChangeItem;
        }

        private void ChangeItem(Cell cell)
        {
            _currentCell = cell;
        }

        private void Sell()
        {
            if (_currentCell == null || _currentCell.Item == null)
                return;

            if (_currentCell.NumberItems > 0)
            {
                _currentCell.SubtractNumberItems(1);
                _player.Wallet.AddMoney(_currentCell.Item.Price);

                if (_currentCell.NumberItems <= 0)
                {
                    _currentCell.SetIsCellBusy(false);
                    Destroy(_currentCell.Item.gameObject);
                    _currentCell.Item = null;
                }
            }
        }

        private void OnDestroy()
        {
            _sell.onClick.RemoveListener(Sell);
            _inventory.OnClickedItem -= ChangeItem;
        }
    }
}