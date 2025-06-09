using _Project.ConstructionBuildings.Buildings;
using Assets._Project.Scripts.Core;
using System;
using UnityEngine;

namespace _Project.Enemy
{
    public class FieldOfViewAttack : MonoBehaviour
    {
        [SerializeField] private float _attackRadius;

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

        public bool CheckPlayerInRadius()
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, _attackRadius, Layers.LayerPlayer);

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out Player.Player _) || collider.TryGetComponent(out BaseBuilding _))
                {
                    return true;
                }
            }
            return false;
        }
    }
}