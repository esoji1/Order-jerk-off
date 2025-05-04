using _Project.CameraMain;
using UnityEngine;

namespace _Project.Core
{
    public class TransitionLevel : MonoBehaviour
    {
        [SerializeField] private AdaptingColliderResolution _adaptingColliderResolution;

        private float _forward = 13f;
        private float _back = 22f;

        private float _addPositionPlayer = 2.2f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            float deltaY = collision.bounds.center.y - GetComponent<Collider2D>().bounds.center.y;

            if (deltaY > 0)
            {
                Debug.Log("Âîøåë ÑÂÅÐÕÓ");
                if (collision.TryGetComponent(out Player.Player player))
                {
                    player.transform.position -= new Vector3(0f, _addPositionPlayer, 0f);
                    _adaptingColliderResolution.AddTopHeight(-_forward);
                    _adaptingColliderResolution.AddBottomHeight(_back);
                }
            }
            else
            {
                Debug.Log("Âîøåë ÑÍÈÇÓ");

                if (collision.TryGetComponent(out Player.Player player))
                {
                    player.transform.position += new Vector3(0f, _addPositionPlayer, 0f);
                    _adaptingColliderResolution.AddTopHeight(_forward);
                    _adaptingColliderResolution.AddBottomHeight(-_back);
                }
            }
        }
    }
}