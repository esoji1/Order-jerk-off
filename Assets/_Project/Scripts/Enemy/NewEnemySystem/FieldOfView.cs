using _Project.Player;
using System;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public event Action<Player> OnPlayerSpotted;
    public event Action<Player> OnPlayerLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            OnPlayerSpotted?.Invoke(player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            OnPlayerLost?.Invoke(player);
    }
}
