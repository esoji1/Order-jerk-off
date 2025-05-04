using System.Collections;
using UnityEngine;

namespace _Project.ResourceExtraction.FishingRodMining
{
    public class FishingRodMining : BaseMining
    {
        public override IEnumerator Obtain()
        {
            while (Time <= Config.ExtractionTime)
            {
                yield return StartCoroutine(AttackMeleeView.StartAttack(PointWeapon.transform, -90, Config.MiningSpeed));

                yield return new WaitForSeconds(Config.ExtractionTime);

                yield return StartCoroutine(AttackMeleeView.StartAttack(PointWeapon.transform, 0, Config.MiningSpeed));
            }
            StopObtain();
        }
    }
}