using UnityEngine;

namespace BallBlastClone
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        private int _damage;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }

        private void Update()
        {
            transform.position += transform.up * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent<Destuctible>(out Destuctible destuctible))
            {
                destuctible.ApplyDamage(_damage);
            }

            Destroy(gameObject);
        }

        public void SetDamage(int damage)
        {
            this._damage = damage;
        }


    }
}