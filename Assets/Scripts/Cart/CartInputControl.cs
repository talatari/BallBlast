using UnityEngine;

namespace BallBlastClone
{
    public class CartInputControl : MonoBehaviour
    {
        [SerializeField] private Cart _cart;
        [SerializeField] private Turret _turret;

        private void Update()
        {
            _cart.SetMovementTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetKey(KeyCode.Space) == true || Input.GetMouseButton(0) == true)
            {
                _turret.Fire();
            }
        }


    }
}