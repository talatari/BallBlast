using UnityEngine;

namespace BallBlastClone
{
    [RequireComponent(typeof(CoinMovement))]
    public class Coin : Destuctible
    {
        private CoinMovement _coinMovement;

        private void Awake()
        {
            _coinMovement = GetComponent<CoinMovement>();

            Die.AddListener(OnCoinDestroyed);
        }

        private void OnDestroy()
        {
            Die.RemoveListener(OnCoinDestroyed);
        }

        private void OnCoinDestroyed()
        {
            Destroy(gameObject);
        }


    }
}
