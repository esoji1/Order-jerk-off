namespace _Project.Artifacts
{
    public class PeriaptVitality : IArtifact
    {
        private Player.Player _player;

        private int _addHealth = 150;

        public PeriaptVitality(Player.Player player)
        {
            _player = player;
        }

        public void Activate()
        {
            _player.PlayerCharacteristics.Health += _addHealth;
            _player.Health.SetHealth(_player.PlayerCharacteristics.Health);
            _player.HealthView.UpdateParameters();
        }

        public void Deactivate()
        {
            _player.PlayerCharacteristics.Health = _player.PlayerCharacteristics.DefaultHealth;
            _player.Health.SetHealth(_player.PlayerCharacteristics.Health);
            _player.HealthView.UpdateParameters();
        }

        public TypeArtefact GetArtifactType() => TypeArtefact.PeriaptVitality;
    }
}
