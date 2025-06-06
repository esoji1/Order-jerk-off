using UnityEngine;

namespace Assets._Project.Scripts.Core
{
    public class Layers : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerPlayer;
        [SerializeField] private LayerMask _layerEnemy;

        public static LayerMask LayerPlayer;
        public static LayerMask LayerEnemy;

        private void Awake()
        {
            LayerPlayer = _layerPlayer;
            LayerEnemy = _layerEnemy;
        }
    }
}
