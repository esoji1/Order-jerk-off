using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    public class ParticleConrtoller : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleMovement;
        [SerializeField] private Player _player;

        private void Update()
        {
            TriggerWalkingFffect(_player.JoysickForMovement.VectorDirection());
        }

        private void TriggerWalkingFffect(Vector3 direction)
        {
            if (direction != Vector3.zero)
            {
                _particleMovement.Pause();
                _particleMovement.Play();
            }
            else if (direction == Vector3.zero)
            {
                _particleMovement.Stop();
            }
        }
    }
}