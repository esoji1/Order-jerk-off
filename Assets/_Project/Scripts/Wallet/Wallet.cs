using System;

namespace Assets._Project.Scripts.Wallet
{
    public class Wallet
    {
        private int _money;

        public int Money => _money;

        public event Action<int> OnMoneyChange;

        public Wallet(int money) => _money = money;
        
        public void AddMoney(int value)
        {
            if (value < 0)
                return;

            _money += value;
            OnMoneyChange?.Invoke(_money);
        }

        public bool SubtractMoney(int value)
        {
            if (value < 0)
                return false;

            int resultSubtractMoney = _money - value;

            if (resultSubtractMoney >= 0) 
            {
                _money = resultSubtractMoney;
                OnMoneyChange?.Invoke(_money);
                return true;
            }

            return false;
        }
    }
}