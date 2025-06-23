using System;

namespace _Project.Enemy.EnemyView
{
    public class WizardEnemyView : BaseEnemyView
    {
        public override void UpdateRunX(float value) {}
        public override void UpdateRunY(float value) {}

        public override void StartRangedAreaAttack() => Animator.SetBool("RangedAreaAttack", true);
        public override void StopRangedAreaAttack() => Animator.SetBool("RangedAreaAttack", false);

        public override void StartDie() => Animator.Play("Die");
    }
}
