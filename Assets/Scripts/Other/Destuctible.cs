using UnityEngine;
using UnityEngine.Events;

namespace BallBlastClone
{
    public class Destuctible : MonoBehaviour
    {
        [HideInInspector] public UnityEvent Die;
        [HideInInspector] public UnityEvent ChangeHitPoints;

        public int MaxHitPoints;
        private int _hitPoints;
        private bool _isDie = false;

        private void Start()
        {
            _hitPoints = MaxHitPoints;
            ChangeHitPoints.Invoke();
        }

        public void ApplyDamage(int damage)
        {
            _hitPoints -= damage;

            ChangeHitPoints.Invoke();

            if (_hitPoints <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            if (_isDie == true)
            {
                return;
            }

            _hitPoints = 0;
            _isDie = true;

            ChangeHitPoints.Invoke();
            Die.Invoke();
        }

        public int GetHitPoints()
        {
            return _hitPoints;
        }

        public int GetMaxHitPoints()
        {
            return MaxHitPoints;
        }


    }
}