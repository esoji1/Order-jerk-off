using _Project.Enemy.Types;
using System;
using UnityEngine;

namespace _Project.Enemy.Breakers
{
    public class MovementBreaker : MonoBehaviour
    {
        public event Action<MovementBreakReasonType> BreakRequested;

        public void Emit(MovementBreakReasonType reason) =>
            BreakRequested?.Invoke(reason);
    }
}
