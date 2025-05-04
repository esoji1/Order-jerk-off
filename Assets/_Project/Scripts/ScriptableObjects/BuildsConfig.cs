using _Project.ConstructionBuildings.Buildings;
using UnityEngine;
namespace _Project.ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = "BuildsConfig", menuName = "ScriptableObjects/Configs/BuildsConfig")]
    public class BuildsConfig : ScriptableObject
    {
        [field: SerializeField] public BaseBuilding BaseBuildingPrefab { get; private set; }
        [field: SerializeField] public GameObject WindowPrefab { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public float RadiusAttack { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float DelayAttack { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
}
