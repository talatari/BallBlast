using UnityEngine;

namespace BallBlastClone
{
    [RequireComponent(typeof(Stone))]
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin _coinPrefab;

        private Stone _stone;

        private void Start()
        {
            _stone = GetComponent<Stone>();
            _stone.Die.AddListener(OnDropCoin);
        }

        private void OnDestroy()
        {
            _stone.Die.RemoveListener(OnDropCoin);
        }

        private void OnDropCoin()
        {
            int chanceDropCoin = Random.Range(0, 4);

            if (chanceDropCoin == 1)
            {
                Instantiate(_coinPrefab, transform.position, Quaternion.identity);
            }
        }


    }
}