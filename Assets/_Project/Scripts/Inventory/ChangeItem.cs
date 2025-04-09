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
        [SerializeField] private Button _putOn;
        private Inventory _inventory;

        private UseWeapons _useWeapons;

        private Cell _currentCell;
        private Cell _clickCell;

        public void Initialize(UseWeapons useWeapons, Inventory inventory)
        {
            _useWeapons = useWeapons;
            _inventory = inventory;
            _currentCell = GetComponent<Cell>();

            UpdateCurrentWeaponInHand();
            _inventory.OnClickedItem += Change;
            _putOn.onClick.AddListener(PutOn);
        }

        private void Change(Cell cell)
        {
            _clickCell = cell;
        }

        private void UpdateCurrentWeaponInHand()
        {
            if (_useWeapons.Weapon.Config.WeaponTypes == WeaponTypes.WoodenSwordPlayer)
            {
                BaseItem itemSpawn = Instantiate(_data.WoodenSwordItem , _currentCell.transform);
                _currentCell.Item = itemSpawn;
            }
            else if (_useWeapons.Weapon.Config.WeaponTypes == WeaponTypes.WoodenSwordPlayer)
            {
                BaseItem itemSpawn = Instantiate(_data.WoodenAxeItem, _currentCell.transform);
                _currentCell.Item = itemSpawn;
            }
        }

        private void PutOn()
        {
            if (_clickCell == null)
                return;

            WeaponItem weaponClickCell = _clickCell.Item as WeaponItem;
            WeaponItem weaponCurrentCell = _currentCell.Item as WeaponItem;

            if (weaponClickCell.TypeItem == weaponCurrentCell.TypeItem)
            {
                Debug.Log("ѕомен€лись местами одинаковые оружи€");
            }
            else if (weaponClickCell.TypeItem != weaponCurrentCell.TypeItem)
            {
                foreach (Cell cell in _inventory.CellList)
                {
                    WeaponItem weapon = cell.Item as WeaponItem;
                    if (cell.Item == null || _currentCell.Item == null)
                        continue;
                    else if (weapon.TypeItem == weaponClickCell.TypeItem)
                    {
                        foreach (Cell cell2 in _inventory.CellList)
                        {
                            WeaponItem weaponCell2 = cell2.Item as WeaponItem;
                            if (cell2.IsCellBusy == false)
                            {
                                if (weaponCell2.TypeItem == WeaponTypes.WoodenSwordPlayer)
                                {
                                    _inventory.AddItemInCell(_data.WoodenSwordItem);
                                    break;
                                }
                                else if (weaponCell2.TypeItem == WeaponTypes.WoodenAxePlayer)
                                {
                                    _inventory.AddItemInCell(_data.WoodenAxeItem);
                                    break;
                                }
                            }

                            else if (weaponCell2.TypeItem == weaponCurrentCell.TypeItem)
                            {
                                cell2.AddNumberItems(1);
                                break;
                            }
                        }

                        if (cell.NumberItems > 0)
                        {
                            _currentCell.Item.gameObject.GetComponent<Image>().sprite = _clickCell.Item.Sprite;
                            _currentCell.Item.Sprite = _clickCell.Item.Sprite;
                            _currentCell.Item.Id = _clickCell.Item.Id;
                            _currentCell.Item.Name = _clickCell.Item.Name;
                            _currentCell.Item.Category = _clickCell.Item.Category;
                            weaponCurrentCell.TypeItem = weapon.TypeItem;
                            cell.SubtractNumberItems(1);
                            _useWeapons.SetWeapon(weaponCurrentCell.TypeItem);

                            if (_clickCell.NumberItems <= 0)
                            {
                                cell.SetIsCellBusy(false);
                                _clickCell.Item = cell.Item;
                                Destroy(cell.Item.gameObject);
                            }
                        }
                    }
                }
            }
        }

        private void OnDestroy()
        {
            _inventory.OnClickedItem += Change;
            _putOn.onClick.RemoveListener(PutOn);
        }
    }
}