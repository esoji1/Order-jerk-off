using _Project.Artifacts;

namespace _Project.SelectionGags
{
    public interface IGagsPicker
    {
        void AddExperience(int value);
        void AddMoney(int value);
        void AddArtifact(TypeArtefact type);
    }
}
