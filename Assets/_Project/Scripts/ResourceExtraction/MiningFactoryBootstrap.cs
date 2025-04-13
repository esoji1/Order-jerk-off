using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Sctipts.Core;
using UnityEngine;

namespace Assets._Project.Sctipts.ResourceExtraction
{
    public class MiningFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private MiningConfig _pickConfig;
        [SerializeField] private PointAttack _pointWeapon;

        private MiningFactory _factory;

        public MiningFactory MiningFactory => _factory;

        private void Awake()
        {
            _factory = new MiningFactory(_pointWeapon, _pickConfig);
        }
    }
}