using System;
using UnityEngine;

namespace Assets.Utils
{
    public class BulletController : MonoBehaviour
    {
        public float DespawnDistance = 20f;
        public GameObject Player;
        public float Speed = 8f;

        private void Update()
        {
            // Move in the right direction
            var direction = transform.rotation*Vector3.up;
            transform.position += direction*Time.deltaTime*Speed;

            // If we're too far away, despawn
            var pos = transform.position;
            var otherPos = Player.transform.position;
            if (Math.Abs(pos.x - otherPos.x) > DespawnDistance || Math.Abs(pos.y - otherPos.y) > DespawnDistance)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
            other.SendMessage("Damage", SendMessageOptions.DontRequireReceiver);
        }
    }
}