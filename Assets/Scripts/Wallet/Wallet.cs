using UnityEngine;

namespace BallBlastClone
{
    public class Wallet
    {
        public delegate void WalletHandler(object sender, int oldCoinsValue, int newCoinsValue);
        public event WalletHandler OnCoinsValueChangedEvent;

        public int Coins { get; private set; }
        private int _defaultCoins = 0;

        public void AddCoins(object sender, int amount)
        {
            var oldCoinsValue = LoadCoinCollected();
            var newCoinsValue = oldCoinsValue + amount;

            this.OnCoinsValueChangedEvent?.Invoke(sender, oldCoinsValue, newCoinsValue);

            SaveCoinCollected(newCoinsValue);
        }

        public void SpendCoins(object sender, int amount)
        {
            var oldCoinsValue = LoadCoinCollected();
            var newCoinsValue = oldCoinsValue - amount;

            this.OnCoinsValueChangedEvent?.Invoke(sender, oldCoinsValue, newCoinsValue);

            SaveCoinCollected(newCoinsValue);
        }

        public bool IsEnoughCoins(int amount)
        {
            Coins = LoadCoinCollected();
            return amount <= Coins;
        }

        private void SaveCoinCollected(int amount)
        {
            PlayerPrefs.SetInt("Wallet:CoinsCollected", amount);
        }

        public int LoadCoinCollected()
        {
            return PlayerPrefs.GetInt("Wallet:CoinsCollected", _defaultCoins);
        }


    }
}