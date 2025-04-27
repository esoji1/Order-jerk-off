using UnityEngine;

namespace Assets._Project.Scripts.Core
{
    public class Move
    {
        public void MoveTarget(Transform targetPosition, Transform currentTransform, float speed)
        {
            currentTransform.position = Vector2.MoveTowards(currentTransform.position, targetPosition.position, speed * Time.deltaTime);
        }
    }
}
