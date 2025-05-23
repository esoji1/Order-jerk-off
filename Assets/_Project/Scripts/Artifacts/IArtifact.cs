namespace _Project.Artifacts
{
    public interface IArtifact
    {
        void Activate();
        void Deactivate();
        TypeArtefact GetArtifactType();
    }
}