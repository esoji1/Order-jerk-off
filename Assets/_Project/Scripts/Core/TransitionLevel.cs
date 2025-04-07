using Assets._Project.Scripts.Player;
using UnityEngine;

namespace Assets._Project.Sctipts.Core
{
    public class TransitionLevel : MonoBehaviour
    {
        [SerializeField] private AdaptingColliderResolution _adaptingColliderResolution;

        private float _forward = 10;
        private float _back = -22;

        private float _addPositionPlayer = 2.2f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            float deltaY = collision.bounds.center.y - GetComponent<Collider2D>().bounds.center.y;

            if (deltaY > 0)
            {
                Debug.Log("Âîøåë ÑÂÅÐÕÓ");
                if (collision.TryGetComponent(out Player player))
                {
                    player.transform.position -= new Vector3(0f, _addPositionPlayer, 0f);
                    _adaptingColliderResolution.AddTopHeight(-10f);
                    _adaptingColliderResolution.AddBottomHeight(22f);
                }
            }
            else
            {
                Debug.Log("Âîøåë ÑÍÈÇÓ");

                if (collision.TryGetComponent(out Player player))
                {
                    player.transform.position += new Vector3(0f, _addPositionPlayer, 0f);
                    _adaptingColliderResolution.AddTopHeight(10f);
                    _adaptingColliderResolution.AddBottomHeight(-22f);
                }
            }
        }
    }
}