using _Project.Craft;
using _Project.Inventory.Items;
using UnityEngine;

namespace Assets._Project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Improve", menuName = "ScriptableObjects/Improve")]
    public class Improve : ScriptableObject
    {
        [field: SerializeField] public BaseItem Item { get; private set; }
        [field: SerializeField] public Craft ImproveTwoLevel { get; private set; }
        [field: SerializeField] public Craft ImproveThreeLevel { get; private set; }
    }
}