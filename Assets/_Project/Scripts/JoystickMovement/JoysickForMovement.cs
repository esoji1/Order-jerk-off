using UnityEngine;

namespace Assets._Project.Sctipts.JoystickMovement
{
    public class JoysickForMovement : JoystickHandler
    {
        public Vector2 VectorDirection()
        {
            if (InputVector.x != 0 || InputVector.y != 0)
                return new Vector2(InputVector.x, InputVector.y);
            else
                return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}