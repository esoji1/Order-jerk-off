using Assets._Project.Sctipts.ResourceExtraction;
using System.Collections;

namespace Assets._Project.Scripts.ResourceExtraction.ScissorsMining
{
    public class Scissors : BaseMining
    {
        public override IEnumerator Obtain()
        {
            while (Time <= Config.ExtractionTime)
            {
                yield return StartCoroutine(AttackMeleeView.StartAttack(PointWeapon.transform, 20, Config.MiningSpeed));

                yield return StartCoroutine(AttackMeleeView.StartAttack(PointWeapon.transform, -20, Config.MiningSpeed));
            }
            StopObtain();
        }
    }
}