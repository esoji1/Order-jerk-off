using UnityEngine;

namespace _Project.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Configs/PlayerCongif")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float RadiusInteractionWithWorld;
        [field: SerializeField] public int Health;
        [field: SerializeField] public LayerMask LayerEnemy;
        [field: SerializeField] public float VisibilityRadius;
    }
}
