using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Sctipts.ResourceExtraction
{
    public class MiningFactoryBootstrap : MonoBehaviour
    {
        [SerializeField] private MiningConfig _pickConfig;
        [SerializeField] private PointAttack _pointWeapon;

        private MiningFactory _factory;
        BaseMining baseMining;

        private void Awake()
        {
            _factory = new MiningFactory(_pointWeapon, _pickConfig);
            baseMining = _factory.Get(TypesMining.Pick, transform.position);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                baseMining.StartObtain();
                Debug.Log("Начал добывать");
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                baseMining.StopObtain(); 
                Debug.Log("Закончил добывать");
            }

        }
    }
}