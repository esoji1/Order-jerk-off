using _Project.Weapon;
using UnityEngine;

namespace _Project.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public int MinDamage;
        [field: SerializeField] public int MaxDamage;
        [field: SerializeField] public Weapon.Weapons.Weapon Prefab;
        [field: SerializeField] public float AttackSpeed;
        [field: SerializeField] public float RadiusAttack;
        [field: SerializeField] public float ReturnInitialAttackPosition;
        [field: SerializeField] public LayerMask Layer;
        [field: SerializeField] public float VisibilityRadius;
        [field: SerializeField] public WeaponTypes WeaponTypes;
    }
}
