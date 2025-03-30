using Assets._Project.Scripts.SelectionGags;
using UnityEngine;

namespace Assets._Project.Sctipts.Core.Spawns
{
    public class SpawnExperience
    {
        private Experience _experience;
        private int _numberExperience;

        public SpawnExperience(Experience experience, int numberExperience)
        {
            _experience = experience;
            _numberExperience = numberExperience;
        }

        public void Spawn(Transform pointExperience)
        {
            Object.Instantiate(_experience, pointExperience.position, Quaternion.identity, null).Initialize(_numberExperience);
        }
    }
}