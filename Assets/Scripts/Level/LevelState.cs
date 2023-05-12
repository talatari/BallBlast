using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BallBlastClone
{
    public class LevelState : MonoBehaviour
    {
        [SerializeField] private Cart _cart;
        [SerializeField] private StoneSpawner _stoneSpawner;
        [SerializeField] private Text _textLevelProgress;
        [SerializeField] private Text _textNextLevelProgress;

        public UnityEvent LevelPassed;
        public UnityEvent LevelDefeat;
        public int LevelProgress;
        private float _timer;
        private bool _checkLevelPassed;
        private string _labelString;
        private int _defaultLevel = 1;

        private void Awake()
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                _labelString = _textLevelProgress.text;

                LoadLevelProgressGame();

                _textLevelProgress.text = _labelString + LevelProgress.ToString();
                _textNextLevelProgress.text = _labelString + (LevelProgress + 1).ToString();

                _stoneSpawner.Level = LevelProgress;

                _cart.CollisionStone.AddListener(OnCartCollisionStone);
                _stoneSpawner.SpawnStonesCompleted.AddListener(OnSpawnCompleted);
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > 0.25f && _checkLevelPassed == true)
            {
                if (FindObjectsOfType<Stone>().Length == 0 && FindObjectsOfType<Coin>().Length == 0)
                {
                    LevelPassed.Invoke();
                }

                _timer = 0.0f;
            }
        }

        private void OnDestroy()
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                _cart.CollisionStone.RemoveListener(OnCartCollisionStone);
                _stoneSpawner.SpawnStonesCompleted.RemoveListener(OnSpawnCompleted);
            }
        }

        private void OnCartCollisionStone()
        {
            //LevelDefeat.Invoke();
        }

        private void OnSpawnCompleted()
        {
            _checkLevelPassed = true;
        }

        public void SaveLevelProgressGame()
        {
            PlayerPrefs.SetInt("LevelProgressGame:LevelProgress", LevelProgress);
        }

        public void LoadLevelProgressGame()
        {
            LevelProgress = PlayerPrefs.GetInt("LevelProgressGame:LevelProgress", _defaultLevel);
        }

        public void ResetLevelProgressGame()
        {
            PlayerPrefs.DeleteKey("LevelProgressGame:LevelProgress");
        }


    }
}