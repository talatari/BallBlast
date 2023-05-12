using UnityEngine;

namespace BallBlastClone
{
    public class CoinMovement : MonoBehaviour
    {
        [SerializeField] private float _verticalSpeed;

        private Vector3 _velocity;
        private bool _useGravity;

        private void Awake()
        {
            if (Mathf.Sign(transform.position.y) == 1)
            {
                _velocity.y = -1 * Mathf.Sign(transform.position.y) * _verticalSpeed;
            }
            else
            {
                _velocity.y = Mathf.Sign(transform.position.y) * _verticalSpeed;
            }
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_useGravity == true)
            {
                _velocity.y = _velocity.y * Time.deltaTime;
            }

            transform.position += _velocity * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<LevelEdges>(out LevelEdges levelEdges))
            {
                if (levelEdges.EdgeType == EdgeType.Bottom && _velocity.x <= 0)
                {
                    _useGravity = true;
                }
            }
        }


    }
}