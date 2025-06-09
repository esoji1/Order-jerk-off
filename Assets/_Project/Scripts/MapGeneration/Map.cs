using _Project.Core.Points.Map;
using UnityEngine;

namespace _Project.MapGeneration
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private MapType _type;

        private PointMapForward _pointMapForward;
        private PointMapBack _pointMapBack;

        public PointMapForward PointMapForward => _pointMapForward;
        public PointMapBack PointMapBack => _pointMapBack;

        private void Awake()
        {
            _pointMapForward = GetComponentInChildren<PointMapForward>();
            _pointMapBack = GetComponentInChildren<PointMapBack>();
        }
    }
}
