using _Project.ScriptableObjects;
using UnityEngine;

namespace _Project.SelectionGags.Artefacts
{
    public class SpawnArtefact
    {
        private GivesData _givesData;
        private float _artifactDropChance;

        public SpawnArtefact(GivesData givesData, float artifactDropChance)
        {
            _givesData = givesData;
            _artifactDropChance = artifactDropChance;
        }

        public void Spawn(Transform point, int randomArtefact)
        {
            float randomValue = Random.Range(0f, 1f);

            if (randomValue < _artifactDropChance / 100f)
                Object.Instantiate(_givesData.Gives[randomArtefact], point.position, Quaternion.identity, null);
        }
    }
}
