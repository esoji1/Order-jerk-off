using _Project.Core;
using System.Collections;
using UnityEngine;

namespace _Project.Potions
{
    public class InvisibilityPotion : BasePotion
    {
        private bool _isClick;

        private void Awake()
        {
            PotionType = TypesPotion.Invisibility;
            _isClick = true;
        }

        protected override bool ApplyEffect()
        {
            if (_isClick)
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
            Player.SetInvisible(true);
            Player.PlayerView.SpriteRenderer.color = new Color(1f, 1f, 1f, 0.2f);
            //Player.CurrentWeapon.WeaponData.ReturnInitialAttackPosition = EffectValue;

            yield return new WaitForSeconds(SecondaryValue);
            Player.SetInvisible(false);
            Player.PlayerView.SpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            //Player.CurrentWeapon.WeaponData.ReturnInitialAttackPosition = Player.CurrentWeapon.WeaponData.DefaultReturnInitialAttackPosition;
            _isClick = true;
        }
    }
}
