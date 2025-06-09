using _Project.Enemy.Types;
using System;
using UnityEngine;

namespace _Project.Enemy.Breakers
{
    public class AttackBreaker : MonoBehaviour
    {
        public event Action<BreakerEnemyType> BreakRequested;

        public void Emit(BreakerEnemyType type) =>
            BreakRequested?.Invoke(type);
    }
}
