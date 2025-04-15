using Assets._Project.Scripts.ActionButton;
using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Inventory.Items;
using Assets._Project.Scripts.Player;
using Assets._Project.Scripts.ResourceExtraction;
using Assets._Project.Scripts.ResourceExtraction.FishingRodMining;
using Assets._Project.Scripts.ResourceExtraction.OreMining;
using Assets._Project.Scripts.ResourceExtraction.ScissorsMining;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.UseWeapons;
using Assets._Project.Sctipts.Inventory.Items;
using System;
using UnityEngine;

namespace Assets._Project.Sctipts.ResourceExtraction
{
    public class ManagerMining : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private UseWeapons _useWeapons;
        [SerializeField] private ActionButton _actionButton;
        [SerializeField] private MiningFactoryBootstrap _mineringFactoryBootstrap;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private InventoryActive _inventoryActive;
        [SerializeField] private ItemData _itemData;
        [SerializeField] private PercentageFillView _percentageFillView;
        [SerializeField] private Canvas _canvas;

        private SetWeaponPoint _setWeaponPoint;

        private bool _isPick;
        private bool _isFishingRod;
        private bool _isScissor;
        private bool _isDoesExtract;
        private BaseMining _baseMining;
        private float _time;

        private void Awake()
        {
            _setWeaponPoint = new SetWeaponPoint();
        }

        private void Update()
        {
            if (_isDoesExtract)
                _time += Time.deltaTime;
            if (_baseMining != null)
            {
                if (_time >= _baseMining.MiningConfig.ExtractionTime + 0.5f)
                {
                    _time = 0;
                    _isDoesExtract = false;
                    _player.SetMove(true);
                    _useWeapons.ActiveSelfWeapon(true);
                    _useWeapons.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    Destroy(_baseMining.gameObject);
                    _baseMining = null;
                }
            }
        }

        private void OnEnable()
        {
            _actionButton.OnMiningOre += StarOre;
            _actionButton.OnMiningFish += StartFishRod;
            _actionButton.OnMiningGrass += StartScissor;
        }

        private void OnDisable()
        {
            _actionButton.OnMiningOre -= StarOre;
            _actionButton.OnMiningFish -= StartFishRod;
            _actionButton.OnMiningGrass -= StartScissor;
        }

        private void StarOre(Ore ore)
        {
            CheckMiningPickItems();

            if (_isPick)
            {
                if (_baseMining == null)
                {
                    _baseMining = _mineringFactoryBootstrap.MiningFactory.Get(TypesMining.Pick, transform.position);
                    _setWeaponPoint.SetParent(_baseMining.transform, _useWeapons.transform);
                    _setWeaponPoint.Set(_baseMining.transform);
                    _useWeapons.ActiveSelfWeapon(false);

                    if (_time <= _baseMining.MiningConfig.ExtractionTime)
                    {
                        _percentageFillView.StartTimer(_baseMining.MiningConfig.ExtractionTime, ore.transform, _canvas);
                        _isDoesExtract = true;
                        _player.SetMove(false);
                        _baseMining.StartObtain();

                        Enum targetType = ore.GetItemType();
                        foreach (BaseItem item in _itemData.Items)
                            if (item.GetItemType().Equals(targetType))
                                _inventory.AddItemInCell(item);
                    }
                }
            }
        }

        private void StartFishRod(Water water)
        {
            CheckMiningFishRodItems();

            if (_isFishingRod)
            {
                if (_baseMining == null)
                {
                    _baseMining = _mineringFactoryBootstrap.MiningFactory.Get(TypesMining.FishingRod, transform.position);
                    _setWeaponPoint.SetParent(_baseMining.transform, _useWeapons.transform);
                    _setWeaponPoint.Set(_baseMining.transform);
                    _useWeapons.ActiveSelfWeapon(false);

                    if (_time <= _baseMining.MiningConfig.ExtractionTime)
                    {
                        _percentageFillView.StartTimer(_baseMining.MiningConfig.ExtractionTime, water.transform, _canvas);
                        _isDoesExtract = true;
                        _player.SetMove(false);
                        _baseMining.StartObtain();

                        int randomFish = UnityEngine.Random.Range(0, _itemData.FishItems.Count);
                        _inventory.AddItemInCell(_itemData.FishItems[randomFish]);
                    }
                }
            }
        }

        private void StartScissor(Grass grass)
        {
            CheckMiningScissorItems();

            if (_isScissor)
            {
                if (_baseMining == null)
                {
                    _baseMining = _mineringFactoryBootstrap.MiningFactory.Get(TypesMining.Scissors, transform.position);
                    _setWeaponPoint.SetParent(_baseMining.transform, _useWeapons.transform);
                    _setWeaponPoint.Set(_baseMining.transform);
                    _useWeapons.ActiveSelfWeapon(false);

                    if (_time <= _baseMining.MiningConfig.ExtractionTime)
                    {
                        _percentageFillView.StartTimer(_baseMining.MiningConfig.ExtractionTime, grass.transform, _canvas);
                        _isDoesExtract = true;
                        _player.SetMove(false);
                        _baseMining.StartObtain();

                        Enum targetType = grass.GetItemType();
                        foreach (BaseItem item in _itemData.GrassItems)
                            if (item.GetItemType().Equals(targetType))
                                _inventory.AddItemInCell(item);
                    }
                }
            }
        }

        private void CheckMiningPickItems()
        {
            foreach (Cell cell in _inventoryActive.CellList)
            {
                if (cell.Item is MiningItem mining)
                {
                    if (mining.TypesMining == TypesMining.Pick)
                    {
                        _isPick = true;
                        break;
                    }

                    _isPick = false;
                }
                else if(cell.Item == null)
                {
                    _isPick = false;
                }
            }
        }

        private void CheckMiningFishRodItems()
        {
            foreach (Cell cell in _inventoryActive.CellList)
            {
                if (cell.Item is MiningItem mining)
                {
                    if (mining.TypesMining == TypesMining.FishingRod)
                    {
                        _isFishingRod = true;
                        break;
                    }

                    _isFishingRod = false;
                }
                else if (cell.Item == null)
                {
                    _isFishingRod = false;
                }
            }
        }

        private void CheckMiningScissorItems()
        {
            foreach (Cell cell in _inventoryActive.CellList)
            {
                if (cell.Item is MiningItem mining)
                {
                    if (mining.TypesMining == TypesMining.Scissors)
                    {
                        _isScissor = true;
                        break;
                    }

                    _isScissor = false;
                }
                else if (cell.Item == null)
                {
                    _isScissor = false;
                }
            }
        }
    }
}
