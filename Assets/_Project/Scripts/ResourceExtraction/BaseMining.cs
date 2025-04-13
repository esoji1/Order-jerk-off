using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Sctipts.ResourceExtraction
{
    public abstract class BaseMining : MonoBehaviour
    {
        private PointAttack _pointWeapon;
        private MiningConfig _config;

        private AttackMeleeView _attackMeleeView;

        private Coroutine _coroutine;
        private float _time;
        private bool _isDoesExtract;

        private void Update()
        {
            if (_isDoesExtract)
                _time += Time.deltaTime;
        }

        public virtual void Initialize(PointAttack pointWeapon, MiningConfig config)
        {
            _pointWeapon = pointWeapon;
            _config = config;
            _attackMeleeView = new AttackMeleeView();
        }

        public virtual void StartObtain()
        {
            StartCoroutine();
        }

        public virtual void StopObtain()
        {
            StopCoroutine();
        }

        private void StartCoroutine()
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Obtain());
                _isDoesExtract = true;
            }
        }

        private void StopCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _isDoesExtract = false;
                _time = 0;
                _coroutine = null;
            }
        }

        private IEnumerator Obtain()
        {
            while (_time <= _config.ExtractionTime)
            {
                yield return StartCoroutine(_attackMeleeView.StartAttack(_pointWeapon.transform, -90, _config.MiningSpeed));

                yield return StartCoroutine(_attackMeleeView.StartAttack(_pointWeapon.transform, 0, _config.MiningSpeed));
            }
            StopObtain();
        }
    }
}