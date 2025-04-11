using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.UseWeapons;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Inventory.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class ChangeItem : MonoBehaviour
    {
        [SerializeField] private ItemData _data;
        [SerializeField] private Button _putOnButton;

        private Inventory _inventory;

        private UseWeapons _useWeapons;

        private Cell _currentCell;
        private Cell _clickedCell;

        public void Initialize(UseWeapons useWeapons, Inventory inventory)
        {
            _useWeapons = useWeapons;
            _inventory = inventory;
            _currentCell = GetComponent<Cell>();

            UpdateCurrentWeaponInHand();
            _inventory.OnClickedItem += ExchangeMergedItem;
            _putOnButton.onClick.AddListener(PutOn);
        }

        private void ExchangeMergedItem(Cell cell)
        {
            _clickedCell = cell;
        }

        private void UpdateCurrentWeaponInHand()
        {
            WeaponTypes weaponTypes = _useWeapons.Weapon.Config.WeaponTypes;

            if (weaponTypes == WeaponTypes.WoodenSwordPlayer)
            {
                BaseItem itemSpawn = Instantiate(_data.WoodenSwordItem , _currentCell.transform);
                _currentCell.Item = itemSpawn;
            }
            else if (weaponTypes == WeaponTypes.WoodenSwordPlayer)
            {
                BaseItem itemSpawn = Instantiate(_data.WoodenAxeItem, _currentCell.transform);
                _currentCell.Item = itemSpawn;
            }
        }

        private void PutOn()
        {
            if (_clickedCell == null)
                return;

            WeaponItem weaponClickCell = _clickedCell.Item as WeaponItem;
            WeaponItem weaponCurrentCell = _currentCell.Item as WeaponItem;

            if (weaponClickCell.TypeItem == weaponCurrentCell.TypeItem)
                Debug.Log("ѕомен€лись местами одинаковые оружи€");

            else if (weaponClickCell.TypeItem != weaponCurrentCell.TypeItem)
            {
                foreach (Cell cell in _inventory.CellList)
                {
                    if (cell.Item == null || _currentCell.Item == null)
                        continue;

                    WeaponItem weapon = cell.Item as WeaponItem;
                    
                    if (weapon.TypeItem == weaponClickCell.TypeItem)
                    {
                        AddItemChange(weaponCurrentCell);

                        if (cell.NumberItems > 0)
                        {
                            ChangeItemDetails(weaponCurrentCell, weapon, cell);
                            
                            if (_clickedCell.NumberItems <= 0)
                                ClearCellData(cell);
                        }
                    }
                }
            }
        }

        private void AddItemChange(WeaponItem weaponCurrentCell)
        {
            foreach (Cell cell2 in _inventory.CellList)
            {
                WeaponItem weaponCell = cell2.Item as WeaponItem;

                if (cell2.IsCellBusy == false)
                {
                    if (weaponCurrentCell.TypeItem == WeaponTypes.WoodenSwordPlayer)
                    {
                        _inventory.AddItemInCell(_data.WoodenSwordItem);
                        break;
                    }
                    else if (weaponCurrentCell.TypeItem == WeaponTypes.WoodenAxePlayer)
                    {
                        _inventory.AddItemInCell(_data.WoodenAxeItem);
                        break;
                    }
                }
                else if (weaponCell.TypeItem == weaponCurrentCell.TypeItem)
                {
                    cell2.AddNumberItems(1);
                    break;
                }
            }
        }

        private void ChangeItemDetails(WeaponItem weaponCurrentCell, WeaponItem weapon, Cell cell)
        {
            _currentCell.Item.gameObject.GetComponent<Image>().sprite = _clickedCell.Item.Sprite;
            _currentCell.Item.Sprite = _clickedCell.Item.Sprite;
            _currentCell.Item.Id = _clickedCell.Item.Id;
            _currentCell.Item.Name = _clickedCell.Item.Name;
            _currentCell.Item.Category = _clickedCell.Item.Category;
            weaponCurrentCell.TypeItem = weapon.TypeItem;
            SubtractAndExchangeWeapons(weaponCurrentCell, cell);
        }

        private void SubtractAndExchangeWeapons(WeaponItem weaponCurrentCell, Cell cell)
        {
            cell.SubtractNumberItems(1);
            _useWeapons.SetWeapon(weaponCurrentCell.TypeItem);
        }

        private void ClearCellData(Cell cell)
        {
            cell.SetIsCellBusy(false);
            _clickedCell.Item = cell.Item;
            Destroy(cell.Item.gameObject);
        }

        private void OnDestroy()
        {
            _inventory.OnClickedItem += ExchangeMergedItem;
            _putOnButton.onClick.RemoveListener(PutOn);
        }
    }
}