using _Project.Artifacts;

namespace _Project.ScriptArtifacts
{
    public class BootsSpeed : IArtifact
    {
        private Player.Player _player;

        public BootsSpeed(Player.Player player)
        {
            _player = player;
        }

        public void Activate()
        {
            _player.PlayerData.Speed = 2;
            _player.PlayerData.DefaultSpeed = 2;
        }

        public void Deactivate()
        {
            _player.PlayerData.Speed = 0;
            _player.PlayerData.DefaultSpeed = 0;
        }

        public TypeArtefact GetArtifactType() => TypeArtefact.BootsSpeed;
    }
}