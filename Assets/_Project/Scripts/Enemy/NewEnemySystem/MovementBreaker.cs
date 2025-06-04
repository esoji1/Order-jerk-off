using System;
using UnityEngine;

public class MovementBreaker : MonoBehaviour
{
    public event Action<MovementBreakReasonType> BreakRequested;

    public void Emit(MovementBreakReasonType reason) =>
        BreakRequested?.Invoke(reason);
}
