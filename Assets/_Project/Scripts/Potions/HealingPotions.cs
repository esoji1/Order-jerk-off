using UnityEngine;

namespace _Project.Potions
{
    public class HealingPotions : BasePotion
    {
        private void Awake()
        {
            PotionType = TypesPotion.Hilka;
        }

        protected override bool ApplyEffect()
        {
            int currentHealth = Player.Health.HealthValue;
            int maxHealth = Player.PlayerCharacteristics.Health;
            int amountToHeal = Mathf.Clamp(EffectValue, 0, maxHealth - currentHealth);

            if (amountToHeal > 0)
            {
                Player.Health.AddHealth(amountToHeal);
                Player.HealthView.AddHealth(amountToHeal);
                return true;
            }

            return false;
        }
    }
}