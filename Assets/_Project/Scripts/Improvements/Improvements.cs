using _Project.Craft;
using _Project.Inventory;
using _Project.Inventory.ForgeInventory;
using _Project.Inventory.Items;
using Assets._Project.Scripts.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Improvements
{
    public class Improvements : MonoBehaviour
    {
        private const int MaxLevel = 3;

        [SerializeField] private Button _improveButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Improve[] _improveData;

        [SerializeField] private GameObject _window;
        [SerializeField] private Image _iconWeapon;
        [SerializeField] private TextMeshProUGUI _textLevel;

        [Header("Improvements")]
        [Header("Two level")]
        [SerializeField] private int _twoDamage;
        [SerializeField] private int _twoSpeedAttak;
        [Header("Three level")]
        [SerializeField] private int _threeDamage;
        [SerializeField] private int _threeSpeedAttak;

        private Inventory.Inventory _inventory;
        private InventoryForge _inventoryForge;

        private Improve _currentImprove;
        private Cell _clickitem;
        private WeaponItem _weaponItem;
        private Tween _tween;

        private void OnEnable() => _improveButton.onClick.AddListener(Craft);

        private void OnDisable() => _improveButton.onClick.RemoveListener(Craft);

        public void Initialize(Inventory.Inventory inventory, InventoryForge inventoryForge)
        {
            _inventory = inventory;
            _inventoryForge = inventoryForge;

            _inventoryForge.OnClickedItem += ChangeItemImprove;
            _exitButton.onClick.AddListener(Hide);
        }

        private void ChangeItemImprove(Cell cell)
        {
            _clickitem = cell;
            _weaponItem = _clickitem.Item as WeaponItem;
            _iconWeapon.sprite = _weaponItem.Sprite;
            _textLevel.text = $"Уровень оружия: {_weaponItem.Level}";
            Show();
        }

        private void Craft()
        {
            if (HasAllIngredients())
            {
                RemoveIngredients();
            }
        }

        public void Show()
        {
            _window.SetActive(true);
            _tween = _window.transform
                .DOScale(1, 0.5f);
        }

        public void Hide()
        {
            _tween.Kill();

            _window.SetActive(false);
            _window.transform.localScale = new Vector3(0, 0, 0);
        }

        private bool HasAllIngredients()
        {
            int level = 0;

            foreach (Improve improve in _improveData)
            {
                if (improve.Item.GetItemType().Equals(_clickitem.Item.GetItemType()))
                {
                    _weaponItem = _clickitem.Item as WeaponItem;
                    _iconWeapon.sprite = _weaponItem.Sprite;
                    _textLevel.text = $"Уровень оружия: {_weaponItem.Level}";

                    _currentImprove = improve;
                    level = _weaponItem.Level;
                    break;
                }
            }

            if (_weaponItem == null)
                return false;

            if (level == 1)
            {
                bool canImprove = true;

                foreach (QuantityItemCraft requiredItem in _currentImprove.ImproveTwoLevel.List)
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
                        Debug.Log("не хватает чтобы вкачать до 2!");
                        break;
                    }
                }

                if (canImprove)
                {
                    level++;
                    _weaponItem.SetLevel(level);
                    _textLevel.text = $"Уровень оружия: {_weaponItem.Level}";
                    _weaponItem.ImprovementWeaponData.Damage = 10;
                    _weaponItem.ImprovementWeaponData.ReturnInitialAttackPosition = 0.1f;
                    Debug.Log("Хватает чтобы вкачать, ура 2");
                    return true;
                }
            }
            if (level == 2)
            {
                bool canImprove = true;

                foreach (QuantityItemCraft requiredItem in _currentImprove.ImproveThreeLevel.List)
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
                        Debug.Log("не хватает чтобы вкачать до 3!");
                        break;
                    }
                }

                if (canImprove)
                {
                    level++;
                    _weaponItem.SetLevel(level);
                    _textLevel.text = $"Уровень оружия: {_weaponItem.Level}";
                    _weaponItem.ImprovementWeaponData.Damage = 20;
                    _weaponItem.ImprovementWeaponData.ReturnInitialAttackPosition = 0.2f;
                    Debug.Log("Хватает чтобы вкачать, ура 3");
                    return true;
                }
            }

            Debug.Log("Нельзя улучшить оружие");

            return false;
        }


        private void RemoveIngredients()
        {
            if (_currentImprove == null)
                return;

            if (_weaponItem.Level == 2)
            {
                foreach (QuantityItemCraft requiredItem in _currentImprove.ImproveTwoLevel.List)
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
            else if (_weaponItem.Level == 3)
            {
                foreach (QuantityItemCraft requiredItem in _currentImprove.ImproveThreeLevel.List)
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
            _currentImprove = null;
        }

        private void OnDestroy()
        {
            if (_inventoryForge != null)
                _inventoryForge.OnClickedItem -= ChangeItemImprove;
            _exitButton.onClick.RemoveListener(Hide);
        }
    }
}
