using _Project.Core;
using _Project.Improvements;
using _Project.ScriptableObjects.Configs;
using _Project.Weapon.TempData;
using TMPro;
using UnityEngine;

namespace _Project.Weapon.Weapons
{
    [RequireComponent(typeof(AttackWeaponFectory))]
    public abstract class Weapon : MonoBehaviour
    {
        private WeaponConfig _config;
        private Transform _pointRotation;
        private Canvas _canvas;
        private TextMeshProUGUI _textDamage;
        private Player.Player _player;

        private AttackMeleeView _attackMeleeView;
        private Move _move;
        private WeaponData _weaponData;

        private SpriteRenderer _spriteRenderer;

        private ImprovementWeaponData _improvementWeaponData;

        public WeaponConfig Config => _config;
        public AttackMeleeView AttackMeleeView => _attackMeleeView;
        public Transform Point => _pointRotation;
        public Move Move => _move;
        public WeaponData WeaponData => _weaponData;
        public Canvas Canvas => _canvas;
        public TextMeshProUGUI TextDamage => _textDamage;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Player.Player Player => _player;
        public ImprovementWeaponData ImprovementWeaponData => _improvementWeaponData;

        public virtual void Initialize(WeaponConfig config, Transform pointRotation, Canvas canvas, TextMeshProUGUI textDamage, Player.Player player)
        {
            _config = config;
            _pointRotation = pointRotation;
            _canvas = canvas;
            _textDamage = textDamage;
            _player = player;
            _attackMeleeView = new AttackMeleeView();
            _move = new Move();
            _weaponData = new WeaponData();

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetImprovementWeaponData(ImprovementWeaponData improvementWeaponData) => 
            _improvementWeaponData = improvementWeaponData;
    }
}