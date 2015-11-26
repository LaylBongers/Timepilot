using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyShootBehavior : MonoBehaviour
    {
        private float _shootCooldown;

        public GameObject BulletPrefab;

        private void Start()
        {
            _shootCooldown = Random.Range(0f, 2f);
        }

        private void Update()
        {
            _shootCooldown -= Time.deltaTime;

            if (_shootCooldown <= 0)
            {
                var obj = Instantiate(BulletPrefab);
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                var bullet = obj.GetComponent<BulletController>();
                bullet.Player = GameObject.Find("Player");

                _shootCooldown = 2f;
            }
        }
    }
}