using _Project.Artifacts;

namespace _Project.SelectionGags.Artefacts
{
    public class BootsSpeed : Gives
    {
        protected override void Add(IGagsPicker gagsPicker) => 
            gagsPicker.AddArtifact(TypeArtefact.BootsSpeed);
    }
}
