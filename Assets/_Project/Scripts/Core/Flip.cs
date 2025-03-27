using UnityEngine;

namespace Assets._Project.Sctipts.Core
{
    public class Flip
    {
        public void RotateDirections(Vector2 direction, Transform rotation)
        {
            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rotation.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        public void RotateView(Vector2 direction, Transform rotation)
        {
            if (direction.x < 0f)
                rotation.localScale = new Vector3(1, -1, 1);
            else if (direction.x > 0f)
                rotation.localScale = new Vector3(1, 1, 1);
        }
    }
}