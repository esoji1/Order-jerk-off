using System;
using UnityEngine;

namespace _Project.Enemy
{
    public class ReasonCompleteStopAttack : MonoBehaviour
    {
        public event Action<ReasonCompleteStopAttackType> BreakRequested;

        public void Emit(ReasonCompleteStopAttackType reason) =>
            BreakRequested?.Invoke(reason);
    }
}