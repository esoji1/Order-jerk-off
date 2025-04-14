using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.ResourceExtraction;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.ResourceExtraction.FishingRodMining
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