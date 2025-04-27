using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyView : MonoBehaviour
    {
        private const string Idle = "Idle";
        private const string Run = "Run";
        private const string Attack = "Attack";
        private const string Die = "Die";

        private Animator _animator;

        public Animator Animator => _animator;

        public void Initialize() => _animator = GetComponent<Animator>();

        public void StartIdle() => _animator.SetBool(Idle, true);
        public void StopIdle() => _animator.SetBool(Idle, false);

        public void StartRun() => _animator.SetBool(Run, true);
        public void StopRun() => _animator.SetBool(Run, false);

        public void StartAttack() => _animator.SetBool(Attack, true);
        public void StopAttack() => _animator.SetBool(Attack, false);

        public void StartDie() => _animator.Play(Die);
    }
}