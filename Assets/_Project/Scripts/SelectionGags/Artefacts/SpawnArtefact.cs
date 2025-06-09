using _Project.ScriptableObjects;
using UnityEngine;

namespace _Project.SelectionGags.Artefacts
{
    public class SpawnArtefact
    {
        private GivesData _givesData;

        public SpawnArtefact(GivesData givesData) => _givesData = givesData;

        public void Spawn(Transform point, int randomArtefact) =>
            Object.Instantiate(_givesData.Gives[randomArtefact], point.position, Quaternion.identity, null);
    }
}
