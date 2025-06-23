using UnityEngine;

namespace _Project.Enemy.EnemyView
{
    [RequireComponent(typeof(Animator))]
    public abstract class BaseEnemyView : MonoBehaviour
    {
        protected Animator Animator;

        public Animator GetAnimator => Animator;

        public void Initialize() => Animator = GetComponent<Animator>();

        public abstract void UpdateRunX(float value);
        public abstract void UpdateRunY(float value);

        public virtual void StartAttack() { }
        public virtual void StopAttack() { }

        public virtual void StartRangeAttack() { }
        public virtual void StopRangeAttack() { }

        public virtual void StartHeavyAttack() { }
        public virtual void StopHeavyAttack() { }

        public virtual void StartRangedAreaAttack() { }
        public virtual void StopRangedAreaAttack() { }

        public abstract void StartDie();
    }
}