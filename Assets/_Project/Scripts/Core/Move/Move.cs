using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Core
{
    public class Move
    {
        public void MoveTarget(Transform targetPosition, Transform currentTransform, float speed)
        {
            currentTransform.position = Vector2.MoveTowards(currentTransform.position, targetPosition.position, speed * Time.deltaTime);
        }

        public void Rotation(Transform spriteRotation, Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spriteRotation.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
