namespace _Project.Artifacts
{
    public class GlovesHaste : IArtifact
    {
        private Player.Player _player;

        private float _returnInitialAttackPosition = 0.3f;

        public GlovesHaste(Player.Player player)
        {
            _player = player;
        }

        public void Activate()
        {
            _player.CurrentWeapon.WeaponData.ReturnInitialAttackPosition += _returnInitialAttackPosition;
        }

        public void Deactivate()
        {
            _player.CurrentWeapon.WeaponData.ReturnInitialAttackPosition = _player.CurrentWeapon.WeaponData.DefaultReturnInitialAttackPosition;
        }

        public TypeArtefact GetArtifactType() => TypeArtefact.GlovesHaste;
    }
}
