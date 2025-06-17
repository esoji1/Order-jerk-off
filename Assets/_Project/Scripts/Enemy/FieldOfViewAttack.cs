using _Project.ConstructionBuildings.Buildings;
using Assets._Project.Scripts.Core;
using System;
using UnityEngine;

namespace _Project.Enemy
{
    public class FieldOfViewAttack : MonoBehaviour
    {
        [SerializeField] private float _attackRadius;

        private BaseBuilding _building;

        public event Action<Transform> OnAttack;
        public event Action<Transform> OnStopAttack;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player _) || collision.TryGetComponent(out BaseBuilding _))
                OnAttack?.Invoke(collision.transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.Player _) || collision.TryGetComponent(out BaseBuilding _))
                OnStopAttack?.Invoke(collision.transform);
        }

        public bool CheckPlayerInRadius()
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, _attackRadius, Layers.LayerPlayer);

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out Player.Player _))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckBaseBuildingInRadius()
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, _attackRadius, Layers.LayerPlayer);

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out BaseBuilding building))
                {
                    _building = building;
                    return true;
                }
            }
            _building = null;
            return false;
        }

        public BaseBuilding ReturnCurrenBuildTarget()
        {
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, _attackRadius, Layers.LayerPlayer);

            foreach (Collider2D collider in collider2D)
            {
                if (collider.TryGetComponent(out BaseBuilding _))
                {
                    return _building;
                }
            }
            return null;
        }
    }
}