using System;
using UnityEngine;

namespace Assets._Project.Scripts.Core
{
    public class SetWeaponPoint
    {
        public void Set(Transform transform)
        {
            transform.localPosition = new Vector3(0f, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.localScale = new Vector3(transform.localScale.x,1, transform.localScale.z);
        }
    }
}
