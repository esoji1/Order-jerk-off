using _Project.Core;
using _Project.ScriptableObjects.Configs;
using _Project.Weapon;
using System.Collections;
using UnityEngine;

namespace _Project.ResourceExtraction
{
    public abstract class BaseMining : MonoBehaviour
    {
        protected MiningConfig Config;
        protected float Time;

        protected PointAttack PointWeapon;
        protected AttackMeleeView AttackMeleeView;

        private Coroutine _coroutine;
        private bool _isDoesExtract;

        public MiningConfig MiningConfig => Config; 

        private void Update()
        {
            if (_isDoesExtract)
                Time += UnityEngine.Time.deltaTime;
        }

        public virtual void Initialize(PointAttack pointWeapon, MiningConfig config)
        {
            PointWeapon = pointWeapon;
            Config = config;
            AttackMeleeView = new AttackMeleeView();
        }

        public void StartObtain()
        {
            StartCoroutine();
        }

        public void StopObtain()
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
                Time = 0;
                _coroutine = null;
            }
        }

        public virtual IEnumerator Obtain()
        {
            while (Time <= Config.ExtractionTime)
            {
                yield return StartCoroutine(AttackMeleeView.StartAttack(PointWeapon.transform, -90, Config.MiningSpeed));

                yield return StartCoroutine(AttackMeleeView.StartAttack(PointWeapon.transform, 0, Config.MiningSpeed));
            }
            StopObtain();
        }
    }
}