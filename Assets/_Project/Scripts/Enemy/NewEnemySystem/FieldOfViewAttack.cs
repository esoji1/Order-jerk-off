using System;
using UnityEngine;

namespace _Project.Enemy
{
    public class FieldOfViewAttack : MonoBehaviour
    {
        public event Action<Player.Player> OnPlayerAttack;
        public event Action<Player.Player> OnPlayerStopAttack;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player player))
                OnPlayerAttack?.Invoke(player);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player player))
                OnPlayerStopAttack?.Invoke(player);
        }
    }
}