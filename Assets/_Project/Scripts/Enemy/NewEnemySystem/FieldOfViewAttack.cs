using _Project.Player;
using System;
using UnityEngine;

public class FieldOfViewAttack : MonoBehaviour
{
    public event Action<Player> OnPlayerAttack;
    public event Action<Player> OnPlayerStopAttack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            OnPlayerAttack?.Invoke(player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            OnPlayerStopAttack?.Invoke(player);
    }
}
