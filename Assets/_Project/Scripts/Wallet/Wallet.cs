using System;

namespace Assets._Project.Scripts.Wallet
{
    public class Wallet
    {
        private int _money;

        public int Money { get; private set; }

        public event Action<int> OnMoneyChange;

        public Wallet(int money) => _money = money;

        public void AddMoney(int value)
        {
            if (value < 0)
                return;

            _money += value;
            OnMoneyChange?.Invoke(_money);
        }

        public void SubtractMoney(int value)
        {
            if (value < 0)
                return;

            int resultSubtractMoney = _money - value;

            if (resultSubtractMoney < _money)
            {
                _money = resultSubtractMoney;
                OnMoneyChange?.Invoke(_money);
            }
        }
    }
}