using _Project.Core;
using _Project.Potions;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Potions
{
    public class DealPoisonDamage : BasePotion
    {
        private bool _isClick;

        private void Awake()
        {
            PotionType = TypesPotion.DeadlyPoison;
            _isClick = true;
        }

        protected override bool ApplyEffect()
        {
            if (_isClick && Player.CurrentWeapon != null)
            {
                CoroutineHelper.Instance.StartCoroutine(TimeUsePotion());
                return true;
            }

            return false;
        }

        private IEnumerator TimeUsePotion()
        {
            if (_isClick == false)
                yield break;

            _isClick = false;
            Player.CurrentWeapon.WeaponData.IsDealPoisonDamage = true;

            yield return new WaitForSeconds(SecondaryValue);

            Player.CurrentWeapon.WeaponData.IsDealPoisonDamage = false;
            _isClick = true;
        }
    }
}
