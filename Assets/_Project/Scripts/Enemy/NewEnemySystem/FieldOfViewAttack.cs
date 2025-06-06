using System;
using UnityEngine;

namespace _Project.Enemy
{
    public class FieldOfViewAttack : MonoBehaviour
    {
        public event Action OnPlayerAttack;
        public event Action OnPlayerStopAttack;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player _))
                OnPlayerAttack?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player _))
                OnPlayerStopAttack?.Invoke();
        }
    }
}