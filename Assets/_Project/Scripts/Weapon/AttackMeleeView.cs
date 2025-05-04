using System.Collections;
using UnityEngine;

namespace _Project.Weapon
{
    public class AttackMeleeView
    {
        public IEnumerator StartAttack(Transform transform, float endDegree, float attackRotationSpeed)
        {
            Vector3 startRotation = transform.localEulerAngles;
            Vector3 targetRotation = new Vector3(0f, 0f, endDegree);

            float progress = 0f;

            while (progress < 1f)
            {
                progress += Time.deltaTime * attackRotationSpeed; 

                transform.localRotation = Quaternion.Lerp(Quaternion.Euler(startRotation), Quaternion.Euler(targetRotation), progress);
                yield return null;
            }
        }
    }
}
