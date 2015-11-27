using UnityEngine;

namespace Assets.Utils
{
    public class DamageOnHit : MonoBehaviour
    {
        public bool DamageSelf = true;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (DamageSelf)
            {
                Destroy(gameObject);
            }

            other.SendMessage("Damage", SendMessageOptions.DontRequireReceiver);
        }
    }
}