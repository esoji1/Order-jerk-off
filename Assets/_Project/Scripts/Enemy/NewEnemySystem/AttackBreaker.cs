using System;
using UnityEngine;

namespace _Project.Enemy
{
    public class AttackBreaker : MonoBehaviour
    {
        public event Action<AttackBreakReasonType> BreakRequested;

        public void Emit(AttackBreakReasonType reason) =>
            BreakRequested?.Invoke(reason);
    }
}