using UnityEngine;

namespace _Project.Core
{
    public class CreatingPrimitive
    {
        private GameObject _primitive;

        private GameObject _createdPrimitive;
        private SpriteRenderer _spriteRenderer;

        public GameObject CreatedPrimitive => _createdPrimitive;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public CreatingPrimitive(GameObject createdPrimitive) => _primitive = createdPrimitive;

        public void CreatePrimitive(Transform transform, float scale)
        {
            if(_createdPrimitive != null)
                GameObject.Destroy(_createdPrimitive);

            _createdPrimitive = GameObject.Instantiate(_primitive, transform.position, Quaternion.identity, null);
            _createdPrimitive.transform.localScale = new Vector3(scale, scale, scale);
            _spriteRenderer = _createdPrimitive.GetComponent<SpriteRenderer>();
        }
    }
}
