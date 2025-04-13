using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Sctipts.Core;
using System;
using UnityEngine;

namespace Assets._Project.Sctipts.ResourceExtraction
{
    public class MiningFactory
    {
        private PointAttack _pointWeapon;
        private MiningConfig _pickConfig;

        public MiningFactory(PointAttack pointWeapon, MiningConfig pickConfig)
        {
            _pointWeapon = pointWeapon;
            _pickConfig = pickConfig;
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

                default:
                    throw new ArgumentException(nameof(typesMining));
            }
        }

        private BaseMining InitializeObject(BaseMining instance, MiningConfig config)
        {
            instance.Initialize(_pointWeapon, config);
            return instance;

            //if (instance is IMeleeAttack)
            //{
            //    instance.Initialize(config, point);
            //    return instance;
            //}
            //else
            //{
            //    throw new ArgumentException(nameof(instance));
            //}
        }
    }
}