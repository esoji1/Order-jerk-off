using _Project.Artifacts;

namespace _Project.SelectionGags.Artefacts
{
    public class GlawsAttack : Gives
    {
        protected override void Add(IGagsPicker gagsPicker) => gagsPicker.AddArtifact(TypeArtefact.ClawsAttack);
    }
}
