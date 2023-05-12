using UnityEngine;
using UnityEngine.UI;

namespace BallBlastClone
{
    public class LevelProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private Text _textPercentsProgressBar;

        private int _levelStoneAmount;
        private int _weightStone;
        private string _fillPercent;
        private float _fillStep;

        private void Awake()
        {
            _progressBar.fillAmount = 0.0f;
        }

        public void CalculatingLevelProgress(int sizeStone)
        {
            switch (sizeStone)
            {
                case 5:
                    _weightStone = 31;
                    break;

                case 4:
                    _weightStone = 15;
                    break;

                case 3:
                    _weightStone = 7;
                    break;

                case 2:
                    _weightStone = 3;
                    break;

                default:
                    _weightStone = sizeStone;
                    break;
            }

            _levelStoneAmount += _weightStone;
        }

        public void FillAmountProgressBar(int damage)
        {
            _fillStep = (float)damage / _levelStoneAmount;

            _progressBar.fillAmount += _fillStep;

            if (_progressBar.fillAmount > 0.99f)
            {
                _progressBar.fillAmount += 0.01f;
            }

            DrawFillProgressBar();
        }

        private void DrawFillProgressBar()
        {
            _fillPercent = (_progressBar.fillAmount * 100).ToString();

            _textPercentsProgressBar.text = _fillPercent.Split('.')[0] + "%";
        }


    }
}