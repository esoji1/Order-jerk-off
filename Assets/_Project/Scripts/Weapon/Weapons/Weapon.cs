using Assets._Project.Scripts.ScriptableObjects.Configs;
using UnityEngine;

namespace Assets._Project.Scripts.Weapon.Weapons
{
    [RequireComponent(typeof(AttackWeaponFectory))]
    public abstract class Weapon : MonoBehaviour
    {
        private WeaponConfig _config;
        private Player.Player _player;
        private Transform _point;

        private AttackMeleeView _attackMeleeView;

        public WeaponConfig Config => _config;
        public AttackMeleeView AttackMeleeView => _attackMeleeView;
        public Player.Player Player => _player;
        public Transform Point => _point;

        public virtual void Initialize(WeaponConfig config, Player.Player player, Transform point)
        {
            _config = config;
            _player = player;
            _point = point;
            _attackMeleeView = new AttackMeleeView();
        }
    }
}