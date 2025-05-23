namespace _Project.Artifacts
{
    public class AncentJanggoEndurance : IArtifact
    {
        private Player.Player _player;

        private float _returnInitialAttackPosition = 0.3f;

        public AncentJanggoEndurance(Player.Player player)
        {
            _player = player;
        }

        public void Activate()
        {
            _player.CurrentWeapon.WeaponData.ReturnInitialAttackPosition += _returnInitialAttackPosition; 
            _player.PlayerData.Speed = 2;
            _player.PlayerData.DefaultSpeed = 2;
        }

        public void Deactivate()
        {
            _player.CurrentWeapon.WeaponData.ReturnInitialAttackPosition = _player.CurrentWeapon.WeaponData.DefaultReturnInitialAttackPosition;
            _player.PlayerData.Speed = 0;
            _player.PlayerData.DefaultSpeed = 0;
        }

        public TypeArtefact GetArtifactType() => TypeArtefact.AncentJanggoEndurance;
    }
}
