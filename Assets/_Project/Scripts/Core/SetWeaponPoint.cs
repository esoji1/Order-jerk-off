using UnityEngine;

namespace Assets._Project.Scripts.Core
{
    public class SetWeaponPoint
    {
        public void SetParent(Transform currentItem, Transform parent) =>
            currentItem.SetParent(parent);

        public void Set(Transform transform)
        {
            transform.localPosition = new Vector3(0f, 0f, 0f);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
