using UnityEngine;

namespace Assets._Project.Scripts.SelectionGags
{
    public class SpawnCoin
    {
        private Coin _coin;
        private int _numberCoin;

        public SpawnCoin(Coin coin, int numberCoin)
        {
            _coin = coin;
            _numberCoin = numberCoin;
        }

        public void Spawn(Transform pointExperience) =>
            Object.Instantiate(_coin, pointExperience.position, Quaternion.identity, null).Initialize(_numberCoin);
    }
}
