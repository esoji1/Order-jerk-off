using _Project.Core;
using _Project.ScriptableObjects.Configs;
using UnityEngine;

namespace _Project.ResourceExtraction
{
    public class MiningFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private MiningConfig _pickConfig, _fishingRodConfig, _scissorsConfig;
        [SerializeField] private PointAttack _pointWeapon;

        private MiningFactory _factory;

        public MiningFactory MiningFactory => _factory;

        private void Awake()
        {
            _factory = new MiningFactory(_pointWeapon, _pickConfig, _fishingRodConfig, _scissorsConfig);
        }
    }
}