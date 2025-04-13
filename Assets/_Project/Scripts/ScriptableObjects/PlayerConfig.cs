using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Configs/PlayerCongif")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed;
        [field: SerializeField] public float VisibilityRadius;
        [field: SerializeField] public int Health;
        [field: SerializeField] public LayerMask LayerEnemy;
    }
}