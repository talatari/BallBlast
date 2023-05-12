using UnityEngine;
using UnityEngine.UI;

namespace BallBlastClone
{
    public class UICoinScore : MonoBehaviour
    {
        [SerializeField] private Cart _cart;
        [SerializeField] private Text _coinScoreText;
        [SerializeField] private LevelState _levelState;

        public Wallet Wallet { get; private set; }
        private WalletCoinPanel _walletCoinPanel;
        private int _coins;

        public UICoinScore()
        {
            this.Wallet = new Wallet();
            this._walletCoinPanel = new WalletCoinPanel(this.Wallet);
        }

        private void Awake()
        {
            _cart.CollisionCoin.AddListener(OnCartCollisionCoin);

            UpdateScoreText();
        }

        private void OnDestroy()
        {
            _cart.CollisionCoin.RemoveListener(OnCartCollisionCoin);
        }

        private void OnCartCollisionCoin()
        {
            AddCoinToWallet(Wallet);

            UpdateScoreText();
        }

        public void UpdateScoreText()
        {
            _coinScoreText.text = this.Wallet.LoadCoinCollected().ToString();
        }

        public void AddCoinToWallet(Wallet wallet)
        {
            this.Wallet = wallet;
            this.Wallet.AddCoins(this, 1);
        }


    }
}