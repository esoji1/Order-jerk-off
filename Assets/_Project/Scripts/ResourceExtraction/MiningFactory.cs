using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Sctipts.Core;
using System;
using UnityEngine;

namespace Assets._Project.Sctipts.ResourceExtraction
{
    public class MiningFactory
    {
        private PointAttack _pointWeapon;
        private MiningConfig _pickConfig, _fishingRodConfig, _scissorsConfig;

        public MiningFactory(PointAttack pointWeapon, MiningConfig pickConfig, MiningConfig fishingRodConfig, MiningConfig scissorsConfig)
        {
            _pointWeapon = pointWeapon;
            _pickConfig = pickConfig;
            _fishingRodConfig = fishingRodConfig;
            _scissorsConfig = scissorsConfig;
        }

        public BaseMining Get(TypesMining typesMining, Vector3 position)
        {
            MiningConfig config = GetConfigBy(typesMining);
            BaseMining instance = UnityEngine.Object.Instantiate(config.Prefab, position, Quaternion.identity, null);
            BaseMining baseWeapon = InitializeObject(instance, config);
            return baseWeapon;
        }

        private MiningConfig GetConfigBy(TypesMining typesMining)
        {
            switch (typesMining)
            {
                case TypesMining.Pick:
                    return _pickConfig;

                case TypesMining.FishingRod:
                    return _fishingRodConfig;

                case TypesMining.Scissors:
                    return _scissorsConfig;

                default:
                    throw new ArgumentException(nameof(typesMining));
            }
        }

        private BaseMining InitializeObject(BaseMining instance, MiningConfig config)
        {
            instance.Initialize(_pointWeapon, config);
            return instance;
        }
    }
}