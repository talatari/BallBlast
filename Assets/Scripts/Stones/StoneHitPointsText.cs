using UnityEngine;
using UnityEngine.UI;

namespace BallBlastClone
{
    [RequireComponent(typeof(Destuctible))]
    public class StoneHitPointsText : MonoBehaviour
    {
        [SerializeField] private Text _hitPointsText;

        private Destuctible _destuctible;

        private void Awake()
        {
            _destuctible = GetComponent<Destuctible>();

            _destuctible.ChangeHitPoints.AddListener(OnChangeHitPoints);
        }

        private void OnDestroy()
        {
            _destuctible.ChangeHitPoints.RemoveListener(OnChangeHitPoints);
        }

        private void OnChangeHitPoints()
        {
            int hitPoints = _destuctible.GetHitPoints();

            if (hitPoints >= 1000)
            {
                _hitPointsText.text = hitPoints / 1000 + "k";
            }
            else
            {
                _hitPointsText.text = hitPoints.ToString();
            }
        }


    }
}