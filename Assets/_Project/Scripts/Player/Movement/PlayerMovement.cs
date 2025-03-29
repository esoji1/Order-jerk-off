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
            if (_player.WallDetection[0].IsTouchingWall == false || _player.WallDetection[1].IsTouchingWall == false)
                return;

            Vector2 movement = _joysickForMovement.VectorDirection() * _player.Speed * Time.deltaTime;
            _player.transform.Translate(movement);
        }

        public void MoveTarget(Transform target)
        {
            _player.transform.position = Vector2.MoveTowards(_player.transform.position, 
                target.position,_player.Config.Speed * Time.deltaTime);
        }
    }
}