using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyDamageBehavior : MonoBehaviour
    {
        private void Damage()
        {
            Destroy(gameObject);
        }
    }
}