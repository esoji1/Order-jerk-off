using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Configs/PlayerCongif")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public int Speed;
        [field: SerializeField] public float AttackRadius;
        [field: SerializeField] public int Health;
        [field: SerializeField] public LayerMask LayerEnemy;
    }
}