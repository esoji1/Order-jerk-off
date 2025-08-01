using _Project.Enemy.Types;
using System;
using UnityEngine;

namespace _Project.Enemy.Breakers
{
    public class ReasonCompleteStopAttack : MonoBehaviour
    {
        public event Action<BreakerEnemyType> BreakRequested;
        public event Action<BreakerEnemyType> StartingRequested;

        public void Emit(BreakerEnemyType type) =>
            BreakRequested?.Invoke(type);

        public void EmitStarting(BreakerEnemyType type) =>
            StartingRequested?.Invoke(type);
    }
}