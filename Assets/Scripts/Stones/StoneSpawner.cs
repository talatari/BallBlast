using UnityEngine;
using UnityEngine.Events;

namespace BallBlastClone
{
    public class StoneSpawner : MonoBehaviour
    {
        [Header("Stones Spawn")]
        [SerializeField] private Stone _stonePrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _spawRate;

        [Header("Balance Spawn")]
        [SerializeField] private Turret _turret;
        [SerializeField][Range(0.0f, 1.0f)] private float _minHitPointsPercent;
        [SerializeField] private float _maxHitPointsRate;
        [SerializeField] private LevelProgressBar _levelProgressBar;

        public UnityEvent SpawnStonesCompleted;
        public int Level;
        private int _stoneMaxHitPoints;
        private int _stoneMinHitPoints;
        private float _timer;
        private int _amountStonesSpawned;
        private int _sizeStone;
        private int _amountStones;

        private void Start()
        {
            _timer = _spawRate;

            _amountStones = GetMaxAmountStones(Level);
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _spawRate)
            {
                SpawnStone();
                _timer = 0;
            }

            if (_amountStonesSpawned == _amountStones)
            {
                enabled = false;
                SpawnStonesCompleted.Invoke();
            }
        }

        private void SpawnStone()
        {
            Stone stone = Instantiate(
                _stonePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);

            int maxRangStone = GetMaxRangStone(Level);
            _sizeStone = Random.Range(1, maxRangStone);

            _levelProgressBar.CalculatingLevelProgress(_sizeStone + 1);

            stone.MaxHitPoints = _sizeStone + 1;
            stone.SetSizeStone((SizeStone)_sizeStone);
            stone.SetColorStone();

            _amountStonesSpawned++;
        }

        private int GetMaxRangStone(int level)
        {
            if (level < 5)
            {
                return 2;
            }
            else if (level > 5 && level < 10)
            {
                return 3;
            }
            else if (level > 10 && level < 15)
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }

        private int GetMaxAmountStones(int level)
        {
            if (level < 10)
            {
                return 2;
            }
            else if (level > 10 && level < 15)
            {
                return 3;
            }
            else if (level > 15 && level < 20)
            {
                return 4;
            }
            else if (level > 20 && level < 25)
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }


    }
}