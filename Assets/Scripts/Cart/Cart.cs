using UnityEngine;
using UnityEngine.Events;

namespace BallBlastClone
{
    public class Cart : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _movementSpeedTurret;
        [SerializeField] private float _turretWidth;

        [Header("Wheels")]
        [SerializeField] private Transform[] _wheels;
        [SerializeField] private float _wheelRadius;

        [HideInInspector] public UnityEvent CollisionStone;
        [HideInInspector] public UnityEvent CollisionCoin;

        private Vector3 _movementTargetTurret;
        private float _deltaMovement;
        private float _lastPositionX;

        private void Start()
        {
            _movementTargetTurret = transform.position;
        }

        private void Update()
        {
            Move();

            RotateWheel();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnCollisionStone(collision);

            OnCollisionCoin(collision);
        }

        private void OnCollisionStone(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent<Stone>(out Stone stone))
            {
                CollisionStone.Invoke();
            }
        }

        private void OnCollisionCoin(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent<Coin>(out Coin coin))
            {
                CollisionCoin.Invoke();
                coin.Die.Invoke();
            }
        }

        private void Move()
        {
            _lastPositionX = transform.position.x;

            transform.position = Vector3.MoveTowards(
                transform.position, _movementTargetTurret, _movementSpeedTurret * Time.deltaTime);

            _deltaMovement = transform.position.x - _lastPositionX;
        }

        private void RotateWheel()
        {
            float angle = (180 * _deltaMovement) / (Mathf.PI * _wheelRadius * 2);

            for (int i = 0; i < _wheels.Length; i++)
            {
                _wheels[i].Rotate(0, 0, -1 * angle);
            }
        }

        public void SetMovementTarget(Vector3 target)
        {
            _movementTargetTurret = ClampMovementTarget(target);
        }

        private Vector3 ClampMovementTarget(Vector3 target)
        {
            float leftBorder = LevelBoundary.Instance.LeftBorder + _turretWidth * 0.5f;
            float rightBorder = LevelBoundary.Instance.RightBorder - _turretWidth * 0.5f;

            Vector3 moveTarget = target;

            moveTarget.z = transform.position.z;
            moveTarget.y = transform.position.y;

            if (moveTarget.x < leftBorder)
            {
                moveTarget.x = leftBorder;
            }

            if (moveTarget.x > rightBorder)
            {
                moveTarget.x = rightBorder;
            }

            return moveTarget;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(
                transform.position - new Vector3(_turretWidth * 0.5f, 0.5f, 0),
                transform.position + new Vector3(_turretWidth * 0.5f, -0.5f, 0));
        }
#endif


    }
}