using _Project.Artifacts;

namespace _Project.SelectionGags.Artefacts
{
    public class GlovesHaste : Gives
    {
        protected override void Add(IGagsPicker gagsPicker) => 
            gagsPicker.AddArtifact(TypeArtefact.GlovesHaste);
    }
}
