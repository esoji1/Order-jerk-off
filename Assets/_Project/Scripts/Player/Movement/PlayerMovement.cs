using Assets._Project.Sctipts.JoystickMovement;
using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    public class PlayerMovement
    {
        private JoysickForMovement _joysickForMovement;
        private Player _player;

        public PlayerMovement(JoysickForMovement joysickForMovement, Player player)
        {
            _joysickForMovement = joysickForMovement;
            _player = player;
        }

        public void Move()
        {
            if (_player.WallDetection.IsTouchingWall == false)
                return;

            Vector2 movement = _joysickForMovement.VectorDirection() * _player.Speed * Time.deltaTime;
            _player.transform.Translate(movement);
        }
    }
}