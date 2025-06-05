using System;
using UnityEngine;

namespace _Project.Enemy
{
    public class FieldOfView : MonoBehaviour
    {
        public event Action<Player.Player> OnPlayerSpotted;
        public event Action<Player.Player> OnPlayerLost;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player player))
                OnPlayerSpotted?.Invoke(player);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player player))
                OnPlayerLost?.Invoke(player);
        }
    }
}