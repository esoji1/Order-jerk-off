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
    }
}
