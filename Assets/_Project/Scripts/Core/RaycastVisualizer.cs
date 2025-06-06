using UnityEngine;

namespace _Project.Core
{
    public class RaycastVisualizer : MonoBehaviour
    {
        [SerializeField] private float _raycastDistance;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _raycastDistance);
        }
    }
}