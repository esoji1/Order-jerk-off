using _Project.Artifacts;

namespace _Project.SelectionGags.Artefacts
{
    public class RingRegeneration : Gives
    {
        protected override void Add(IGagsPicker gagsPicker) => 
            gagsPicker.AddArtifact(TypeArtefact.RingRegeneration);
    }
}
