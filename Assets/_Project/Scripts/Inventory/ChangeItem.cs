using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.UseWeapons;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class ChangeItem : MonoBehaviour
    {
        [SerializeField] private ItemData _data;
        [SerializeField] private Button _putOn;
        [SerializeField] private Inventory _inventory;

        private UseWeapons _useWeapons;

        private Cell _currentCell;
        private Cell _clickCell;


        public void Initialize(UseWeapons useWeapons)
        {
            _useWeapons = useWeapons;
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
            if (_useWeapons.Weapon.Config.WeaponTypes == _data.WoodenSwordItem.WeaponType)
            {
                Item itemSpawn = Instantiate(_data.WoodenSwordItem, _currentCell.transform);
                _currentCell.Item = itemSpawn;
            }
            else if (_useWeapons.Weapon.Config.WeaponTypes == _data.WoodenAxeItem.WeaponType)
            {
                Item itemSpawn = Instantiate(_data.WoodenAxeItem, _currentCell.transform);
                _currentCell.Item = itemSpawn;
            }
        }

        private void PutOn()
        {
            if (_clickCell.Item.WeaponType == _currentCell.Item.WeaponType)
            {
                Debug.Log("ѕомен€лись местами одинаковые оружи€");
            }
            else if (_clickCell.Item.WeaponType != _currentCell.Item.WeaponType)
            {
                foreach (Cell cell in _inventory.CellList)
                {
                    if (cell.Item == null || _currentCell.Item == null)
                        continue;
                    else if (cell.Item.WeaponType == _clickCell.Item.WeaponType)
                    {
                        foreach (Cell cell2 in _inventory.CellList)
                        {
                            if (cell2.IsCellBusy == false)
                            {
                                if (_currentCell.Item.WeaponType == _data.WoodenSwordItem.WeaponType)
                                {
                                    _inventory.AddItemInCell(_data.WoodenSwordItem);
                                    break;
                                }
                                else if (_currentCell.Item.WeaponType == _data.WoodenAxeItem.WeaponType)
                                {
                                    _inventory.AddItemInCell(_data.WoodenAxeItem);
                                    break;
                                }
                            }

                            else if (cell2.Item.WeaponType == _currentCell.Item.WeaponType)
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
                            _currentCell.Item.WeaponType = cell.Item.WeaponType;
                            cell.SubtractNumberItems(1);
                            _useWeapons.SetWeapon(_currentCell.Item.WeaponType);

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