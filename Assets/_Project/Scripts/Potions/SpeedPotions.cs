using _Project.Core;
using System.Collections;
using UnityEngine;

namespace _Project.Potions
{
    public class SpeedPotions : BasePotion
    {
        private bool _isClick;

        private void Awake()
        {
            PotionType = TypesPotion.SpeedUp;
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
            Player.CurrentWeapon.WeaponData.ReturnInitialAttackPosition = EffectValue;

            yield return new WaitForSeconds(SecondaryValue);
            Player.CurrentWeapon.WeaponData.ReturnInitialAttackPosition = Player.CurrentWeapon.WeaponData.DefaultReturnInitialAttackPosition;
            _isClick = true;
        }
    }
}