using UnityEngine;

namespace Assets.Enemy
{
    public class EnemyDamageBehavior : MonoBehaviour
    {
        private void Damage()
        {
            Destroy(gameObject);
        }
    }
}