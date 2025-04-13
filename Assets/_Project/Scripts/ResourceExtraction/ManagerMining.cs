using Assets._Project.Scripts.ActionButton;
using Assets._Project.Scripts.Core;
using Assets._Project.Scripts.Inventory;
using Assets._Project.Scripts.Inventory.Items;
using Assets._Project.Scripts.Player;
using Assets._Project.Scripts.ResourceExtraction.OreMining;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.UseWeapons;
using Unity.VisualScripting;
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
        [SerializeField] private ItemData _itemData;

        private SetWeaponPoint _setWeaponPoint;

        private bool _isPick;
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
                    Destroy(_baseMining.gameObject);
                }
            }
        }

        private void OnEnable()
        {
            _actionButton.OnMining += StartMining;
        }

        private void OnDisable()
        {
            _actionButton.OnMining -= StartMining;
        }

        private void StartMining(Ore ore)
        {
            foreach (Cell cell in _inventory.CellList)
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
            }

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
                        _isDoesExtract = true;
                        _player.SetMove(false);
                        _baseMining.StartObtain();
                        _inventory.AddItemInCell(_itemData.IronOre);
                    }
                }
            }
        }
    }
}
