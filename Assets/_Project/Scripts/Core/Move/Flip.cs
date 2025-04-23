using UnityEngine;

namespace Assets._Project.Sctipts.Core
{
    public class Flip
    {
        public void RotateView(Vector2 direction, Transform rotation)
        {
            if (direction.x < 0f)
                rotation.localScale = new Vector3(-1, 1, 1);
            else if (direction.x > 0f)
                rotation.localScale = new Vector3(1, 1, 1);
        }
    }
}