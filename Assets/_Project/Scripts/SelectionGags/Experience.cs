namespace Assets._Project.Scripts.SelectionGags
{
    public class Experience : Gives
    {
        private int _numberExperience;

        public void Initialize(int numberExperience) => _numberExperience = numberExperience;

        protected override void Add(IGagsPicker gagsPicker) => gagsPicker.AddExperience(_numberExperience);
    }
}
