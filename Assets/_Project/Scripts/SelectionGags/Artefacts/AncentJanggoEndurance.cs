using _Project.Artifacts;

namespace _Project.SelectionGags.Artefacts
{
    public class AncentJanggoEndurance : Gives
    {
        protected override void Add(IGagsPicker gagsPicker) =>
            gagsPicker.AddArtifact(TypeArtefact.AncentJanggoEndurance);
    }
}
