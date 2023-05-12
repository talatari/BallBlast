using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BallBlastClone
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private LevelState _levelState;
        [SerializeField] private UITurretAbilities _uITurretAbilities;
        [SerializeField] private Turret _turret;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape) == true)
                QuitGame();
        }

        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextLevel()
        {
            _levelState.LevelProgress++;

            _levelState.SaveLevelProgressGame();

            ReloadCurrentScene();
        }

        public void LoadLevel(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ResetGameProgress()
        {
            _levelState.ResetLevelProgressGame();

            _turret.ResetTurretAbilities();

            _uITurretAbilities.ResetPriceTurretAbilities();
            _uITurretAbilities.RefreshAbilitiesInfo();
        }


    }
}