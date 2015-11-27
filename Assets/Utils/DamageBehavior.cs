using UnityEngine;

namespace Assets.Utils
{
    public class DamageBehavior : MonoBehaviour
    {
        private void Damage()
        {
            Destroy(gameObject);
        }
    }
}