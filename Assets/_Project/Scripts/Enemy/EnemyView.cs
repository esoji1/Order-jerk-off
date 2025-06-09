using UnityEngine;

namespace _Project.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyView : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Attack = "Attack";
        private const string Die = "Dies";

        private Animator _animator;

        public Animator Animator => _animator;

        public void Initialize() => _animator = GetComponent<Animator>();

        public void UpdateRunX(float value) => _animator.SetFloat(Horizontal, value);
        public void UpdateRunY(float value) => _animator.SetFloat(Vertical, value);

        public void StartAttack() => _animator.SetBool(Attack, true);
        public void StopAttack() => _animator.SetBool(Attack, false);

        public void StartDie() => _animator.Play(Die);
    }
}