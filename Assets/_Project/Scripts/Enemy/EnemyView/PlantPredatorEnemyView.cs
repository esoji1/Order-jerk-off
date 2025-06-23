using System;
using UnityEngine;

namespace _Project.Enemy.EnemyView
{
    public class PlantPredatorEnemyView : BaseEnemyView
    {
    
        public override void UpdateRunX(float value) => Animator.SetFloat("Horizontal", value);
        public override void UpdateRunY(float value) => Animator.SetFloat("Vertical", value);

        public override void StartAttack() => Animator.SetBool("Attack", true);
        public override void StopAttack() => Animator.SetBool("Attack", false);

        public override void StartDie() => Animator.Play("Die");
    }
}
