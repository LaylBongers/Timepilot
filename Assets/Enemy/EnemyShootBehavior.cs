using UnityEngine;

namespace Assets.Enemy
{
    public class EnemyShootBehavior : MonoBehaviour
    {
        private float _shootCooldown;

        public GameObject BulletPrefab;
        public float ShootIntervalMin = 6;
        public float ShootIntervalMax = 16;

        private void Start()
        {
            _shootCooldown = Random.Range(0, ShootIntervalMax);
        }

        private void Update()
        {
            _shootCooldown -= Time.deltaTime;

            if (_shootCooldown <= 0)
            {
                var obj = Instantiate(BulletPrefab);
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;

                _shootCooldown = Random.Range(ShootIntervalMin, ShootIntervalMax);
            }
        }
    }
}