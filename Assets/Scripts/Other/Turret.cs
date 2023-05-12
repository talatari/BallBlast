using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace BallBlastClone
{
    public class Turret : MonoBehaviour
    {
        [Header("Main Setting Turret")]
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _projectileInterval;

        public float FireSpeed;
        public int Damage;
        public int ProjectileAmount;
        private float _timer;

        private void Awake()
        {
            LoadTurretAbilities();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private void SpawnProjectile()
        {
            float _startPositionX = _shootPoint.position.x - _projectileInterval * (ProjectileAmount - 1) * 0.5f;

            for (int i = 0; i < ProjectileAmount; i++)
            {
                Projectile projectile = Instantiate(
                    _projectilePrefab,
                    new Vector3(_startPositionX + i * _projectileInterval, _shootPoint.position.y, _shootPoint.position.z),
                    transform.rotation);

                projectile.SetDamage(Damage);
            }
        }

        public void Fire()
        {
            if (_timer >= FireSpeed)
            {
                SpawnProjectile();

                _timer = 0;
            }
        }

        private void LoadTurretAbilities()
        {
            FireSpeed = PlayerPrefs.GetFloat("TurretAbilitiesFireSpeed:FireSpeed", 0.75f);
            Damage = PlayerPrefs.GetInt("TurretAbilitiesDamage:Damage", 1);
            ProjectileAmount = PlayerPrefs.GetInt("TurretAbilitiesProjectileAmount:ProjectileAmount", 1);
        }

        public void SaveTurretAbilities()
        {
            PlayerPrefs.SetFloat("TurretAbilitiesFireSpeed:FireSpeed", FireSpeed);
            PlayerPrefs.SetInt("TurretAbilitiesDamage:Damage", Damage);
            PlayerPrefs.SetInt("TurretAbilitiesProjectileAmount:ProjectileAmount", ProjectileAmount);
        }

        public void ResetTurretAbilities()
        {
            PlayerPrefs.DeleteKey("TurretAbilitiesFireSpeed:FireSpeed");
            PlayerPrefs.DeleteKey("TurretAbilitiesDamage:Damage");
            PlayerPrefs.DeleteKey("TurretAbilitiesProjectileAmount:ProjectileAmount");
        }


    }
}