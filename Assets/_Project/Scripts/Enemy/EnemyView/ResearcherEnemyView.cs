using _Project.Enemy.EnemyView;

namespace _Project.Enemy.EnemyView
{
    public class ResearcherEnemyView : BaseEnemyView
    {
        public override void UpdateRunX(float value) { }
        public override void UpdateRunY(float value) { }

        public override void StartAttack() => Animator.SetBool("Attack", true);
        public override void StopAttack() => Animator.SetBool("Attack", false);

        public override void StartDie() => Animator.Play("Die");
    }
}
