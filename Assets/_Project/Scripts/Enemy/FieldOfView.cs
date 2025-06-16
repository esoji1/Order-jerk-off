using _Project.ConstructionBuildings.Buildings;
using System;
using UnityEngine;

namespace _Project.Enemy
{
    public class FieldOfView : MonoBehaviour
    {
        public event Action<Transform> OnSpotted;
        public event Action<Transform> OnLost;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player player) || collision.TryGetComponent(out BaseBuilding _))
                OnSpotted?.Invoke(collision.transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player player) || collision.TryGetComponent(out BaseBuilding _))
                OnLost?.Invoke(collision.transform);
        }
    }
}