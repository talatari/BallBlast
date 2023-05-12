using UnityEngine;

namespace BallBlastClone
{
    public class StoneMovement : MonoBehaviour
    {
        [SerializeField] private float _gravity;
        [SerializeField] private float _reboundSpeed;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _gravityOffset;
        [SerializeField] private float _rotationSpeed;

        private Vector3 _velocity;
        private bool _useGravity;

        private void Awake()
        {
            _velocity.x = -1 * Mathf.Sign(transform.position.x) * _horizontalSpeed;
        }

        private void Update()
        {
            TryEnableGravity();

            Move();
        }

        private void Move()
        {
            if (transform.position.x > 15 || transform.position.x < -15)
            {
                Destroy(gameObject);
            }

            if (_useGravity == true)
            {
                _velocity.y -= _gravity * Time.deltaTime;
                transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
            }

            _velocity.x = Mathf.Sign(_velocity.x) * _horizontalSpeed;

            transform.position += _velocity * Time.deltaTime;
        }

        private void TryEnableGravity()
        {
            if (Mathf.Abs(transform.position.x) <= Mathf.Abs(LevelBoundary.Instance.LeftBorder) - _gravityOffset)
            {
                _useGravity = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<LevelEdges>(out LevelEdges levelEdges))
            {
                if (levelEdges.EdgeType == EdgeType.Bottom)
                {
                    _velocity.y = _reboundSpeed;
                }

                if (levelEdges.EdgeType == EdgeType.Left && _velocity.x < 0 ||
                    levelEdges.EdgeType == EdgeType.Right && _velocity.x > 0)
                {
                    _velocity.x *= -1;
                }
            }
        }

        public void AddVerticalVelocity(float velocity)
        {
            this._velocity.y += velocity;
        }

        public void SetHorizontalDirection(float direction)
        {
            _velocity.x = Mathf.Sign(direction) * _horizontalSpeed;
        }


    }
}