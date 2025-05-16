using _Project.Craft;
using _Project.Inventory;
using _Project.Inventory.ForgeInventory;
using _Project.Inventory.Items;
using Assets._Project.Scripts.ScriptableObjects;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Improvements
{
    public class Improvements : MonoBehaviour
    {
        [SerializeField] private Button _improveButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Improve[] _improveData;

        [SerializeField] private GameObject _window;
        [SerializeField] private Image _iconWeapon;
        [SerializeField] private TextMeshProUGUI _textLevel;

        [Header("Improvements")]
        [Header("Two level")]
        [SerializeField] private int _twoDamage;
        [SerializeField] private float _twoSpeedAttak;
        [Header("Three level")]
        [SerializeField] private int _threeDamage;
        [SerializeField] private float _threeSpeedAttak;

        [Header("Display for improvement")]
        [SerializeField] private GameObject _contentTwoImprovement;
        [SerializeField] private GameObject _contentThreeImprovement;

        private Inventory.Inventory _inventory;
        private InventoryForge _inventoryForge;

        private Cell[] _twoCells;
        private Cell[] _threeCells;

        private Improve _currentImprove;
        private Cell _clickitem;
        private WeaponItem _weaponItem;
        private Tween _tween;
        private List<BaseItem> _items;

        private void OnEnable() => _improveButton.onClick.AddListener(Craft);

        private void OnDisable() => _improveButton.onClick.RemoveListener(Craft);

        public void Initialize(Inventory.Inventory inventory, InventoryForge inventoryForge)
        {
            _inventory = inventory;
            _inventoryForge = inventoryForge;
            _items = new List<BaseItem>();

            _twoCells = _contentTwoImprovement.GetComponentsInChildren<Cell>();
            _threeCells = _contentThreeImprovement.GetComponentsInChildren<Cell>();

            _inventoryForge.OnClickedItem += ChangeItemImprove;
            _exitButton.onClick.AddListener(Hide);
        }

        private void ChangeItemImprove(Cell cell)
        {
            _clickitem = cell;
            _weaponItem = _clickitem.Item as WeaponItem;
            UpdateUI();
            FindMatchingImprove();
            CreateImprovementItems();
            Show();
        }

        private void UpdateUI()
        {
            _iconWeapon.sprite = _weaponItem.Sprite;
            _textLevel.text = $"Уровень оружия: {_weaponItem.Level}";
        }

        private void FindMatchingImprove()
        {
            foreach (Improve improve in _improveData)
            {
                if (improve.Item.GetItemType().Equals(_weaponItem.GetItemType()))
                {
                    _currentImprove = improve;
                    break;
                }
            }
        }

        private void CreateImprovementItems()
        {
            ClearCachedItems();

            CreateItemsForLevel(_currentImprove.ImproveTwoLevel, _contentTwoImprovement.transform, _twoCells);
            CreateItemsForLevel(_currentImprove.ImproveThreeLevel, _contentThreeImprovement.transform, _threeCells);
        }

        private void CreateItemsForLevel(Craft.Craft level, Transform parent, Cell[] cells)
        {
            for (int i = 0; i < level.List.Count; i++)
            {
                BaseItem item = Instantiate(level.List[i].Item, cells[i].transform);
                cells[i].NumberItems = level.List[i].Quantity;
                cells[i].AddNumberItems(0);
                _items.Add(item);
            }
        }

        private void ClearCachedItems()
        {
            for (int i = 0; i <= 2; i++)
            {
                _twoCells[i].NumberItems = 0;
                _twoCells[i].AddNumberItems(0);

                _threeCells[i].NumberItems = 0;
                _threeCells[i].AddNumberItems(0);
            }

            foreach (BaseItem item in _items)
                Destroy(item.gameObject);

            _items.Clear();
        }

        private void Craft()
        {
            if (HasAllIngredients())
                RemoveIngredients();
        }

        public void Show()
        {
            _window.SetActive(true);
            _tween = _window.transform
                .DOScale(1, 0.5f);
        }

        public void Hide()
        {
            ClearCachedItems();

            _tween.Kill();
            _window.SetActive(false);
            _window.transform.localScale = new Vector3(0, 0, 0);
        }

        private bool HasAllIngredients()
        {
            foreach (Improve improve in _improveData)
            {
                if (improve.Item.GetItemType().Equals(_clickitem.Item.GetItemType()))
                {
                    _textLevel.text = $"Уровень оружия: {_weaponItem.Level}";
                    _currentImprove = improve;
                    break;
                }
            }

            if (_weaponItem == null)
                return false;

            return _weaponItem.Level switch
            {
                1 => Upgrade(_currentImprove.ImproveTwoLevel, 2, _twoDamage, _twoSpeedAttak),
                2 => Upgrade(_currentImprove.ImproveThreeLevel, 3, _threeDamage, _threeSpeedAttak),
                _ => false
            };
        }

        private bool Upgrade(Craft.Craft upgrade, int level, int damage, float speedAttak)
        {
            bool canImprove = true;

            foreach (QuantityItemCraft requiredItem in upgrade.List)
            {
                int totalInInventory = 0;

                foreach (Cell cell in _inventory.CellList)
                {
                    if (cell.Item == null)
                        continue;

                    if (cell.Item.GetItemType().Equals(requiredItem.Item.GetItemType()))
                    {
                        totalInInventory += cell.NumberItems;
                        if (totalInInventory >= requiredItem.Quantity)
                            break;
                    }
                }

                if (totalInInventory < requiredItem.Quantity)
                {
                    canImprove = false;
                    Debug.Log($"не хватает чтобы вкачать до {level}!");
                    break;
                }
            }

            if (canImprove)
            {
                _weaponItem.SetLevel(level);
                _textLevel.text = $"Уровень оружия: {_weaponItem.Level}";
                _weaponItem.ImprovementWeaponData.Damage = damage;
                _weaponItem.ImprovementWeaponData.ReturnInitialAttackPosition = speedAttak;
                Debug.Log($"Хватает чтобы вкачать, ура {level}");
                return true;
            }

            return false;
        }

        private void RemoveIngredients()
        {
            if (_currentImprove == null)
                return;

            switch (_weaponItem.Level)
            {
                case 2:
                    Remove(_currentImprove.ImproveTwoLevel);
                    break;
                case 3:
                    Remove(_currentImprove.ImproveThreeLevel);
                    break;
            };
    
            _currentImprove = null;
        }

        private void Remove(Craft.Craft upgrade)
        {
            foreach (QuantityItemCraft requiredItem in upgrade.List)
            {
                int remainingToRemove = requiredItem.Quantity;

                foreach (Cell cell in _inventory.CellList)
                {
                    if (remainingToRemove <= 0) break;
                    if (cell.Item == null) continue;

                    if (requiredItem.Item.GetItemType().Equals(cell.Item.GetItemType()))
                    {
                        int canRemove = Mathf.Min(cell.NumberItems, remainingToRemove);
                        _inventory.SubtractItems(cell, canRemove);
                        remainingToRemove -= canRemove;
                    }
                }
            }
        }

        private void OnDestroy()
        {
            if (_inventoryForge != null)
                _inventoryForge.OnClickedItem -= ChangeItemImprove;
            _exitButton.onClick.RemoveListener(Hide);
        }
    }
}