using UnityEngine;

namespace Assets._Project.Scripts.SelectionGags
{
    public abstract class Gives : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IGagsPicker gagsPicker))
            {
                Add(gagsPicker);
                Destroy(gameObject);
            }
        }

        protected abstract void Add(IGagsPicker gagsPicker);
    }
}
