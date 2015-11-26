using UnityEngine;

namespace Assets.Scripts
{
    public class Follower : MonoBehaviour
    {
        public Transform Target;

        private void Start()
        {
        }

        private void Update()
        {
            transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
        }
    }
}