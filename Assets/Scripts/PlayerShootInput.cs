using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerShootInput : MonoBehaviour
    {
        private float _cooldown;

        public GameObject BulletPrefab;
        public float CooldownTime = 0.5f;

        private void Update()
        {
            if (_cooldown <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    var obj = Instantiate(BulletPrefab);
                    obj.transform.position = transform.position;
                    obj.transform.rotation = transform.rotation;
                    var bullet = obj.GetComponent<BulletController>();
                    bullet.Player = gameObject;

                    _cooldown = CooldownTime;
                }
            }
            else
            {
                _cooldown -= Time.deltaTime;
            }
        }
    }
}