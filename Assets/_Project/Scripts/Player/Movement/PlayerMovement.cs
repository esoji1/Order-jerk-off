using _Project.JoystickMovement;
using UnityEngine;

namespace _Project.Player
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
            if (_player.WallDetection[0].IsTouchingWall == false || _player.WallDetection[1].IsTouchingWall == false)
                return;

            Vector2 movement = _joysickForMovement.VectorDirection() * _player.Speed * Time.deltaTime;
            _player.transform.Translate(movement);
        }
    }
}