namespace _Project.Artifacts
{
    public class ClawsAttack : IArtifact
    {
        private Player.Player _player;

        private int _addDamage = 10;

        public ClawsAttack(Player.Player player)
        {
            _player = player;
        }

        public void Activate()
        {
            _player.CurrentWeapon.WeaponData.ExtraDamage += _addDamage;
        }

        public void Deactivate()
        {
            _player.CurrentWeapon.WeaponData.ExtraDamage = _player.CurrentWeapon.WeaponData.DefaultExtraDamage;
        }

        public TypeArtefact GetArtifactType() => TypeArtefact.ClawsAttack;
    }
}
