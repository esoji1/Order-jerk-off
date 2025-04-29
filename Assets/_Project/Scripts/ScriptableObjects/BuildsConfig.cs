using Assets._Project.Scripts.ConstructionBuildings.Buildings;
using UnityEngine;
namespace Assets._Project.Scripts.ScriptableObjects.Configs
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
    }
}
