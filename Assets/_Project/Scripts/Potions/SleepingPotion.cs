using _Project.Core;
using System.Collections;
using UnityEngine;

namespace _Project.Potions
{
    public class SleepingPotion : BasePotion
    {
        private bool _isClick;

        private void Awake()
        {
            PotionType = TypesPotion.Sleeping;
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

            Collider2D[] collider2D = Physics2D.OverlapCircleAll(Player.transform.position, EffectValue, Player.Config.LayerEnemy);

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out Enemy.Behaviors.Enemy enemy))
                {
                    enemy.SetSleeps(true);
                }
            }

            yield return new WaitForSeconds(SecondaryValue);

            foreach (Collider2D collider in collider2D)
            {
                if (collider != null)
                {
                    if (collider.TryGetComponent(out Enemy.Behaviors.Enemy enemy))
                    {
                        enemy.SetSleeps(false);
                    }
                }
            }

            _isClick = true;
        }
    }
}
