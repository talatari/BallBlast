using UnityEngine;

namespace BallBlastClone
{
    public class WalletCoinPanel
    {
        private Wallet _wallet;

        public WalletCoinPanel(Wallet wallet)
        {
            this._wallet = wallet;
            this._wallet.OnCoinsValueChangedEvent += WalletOnCoinsValueChanged;
        }

        private void WalletOnCoinsValueChanged(object sender, int oldCoinValue, int newCoinValue)
        {
            // TODO: Update UI Score Text
        }


    }
}