namespace Assets._Project.Scripts.SelectionGags
{
    public class Coin : Gives
    {
        private int _numberCoin;

        public void Initialize(int numberCoin) => _numberCoin = numberCoin;

        protected override void Add(IGagsPicker gagsPicker) => gagsPicker.AddMoney(_numberCoin);
    }
}
