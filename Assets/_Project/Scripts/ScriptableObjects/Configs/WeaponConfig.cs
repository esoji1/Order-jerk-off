using Assets._Project.Scripts.Weapon;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage;
        [field: SerializeField] public Weapon.Weapons.Weapon Prefab;
        [field: SerializeField] public int AttackSpeed;
        [field: SerializeField] public int Price;
        [field: SerializeField] public float RadiusAttack;
        [field: SerializeField] public float ReturnInitialAttackPosition;
        [field: SerializeField] public LayerMask Layer;
        [field: SerializeField] public float RaycastAttack;
        [field: SerializeField] public float VisibilityRadius;
        [field: SerializeField] public WeaponTypes WeaponTypes;
        [field: SerializeField] public string NameWeapon;
    }
}
