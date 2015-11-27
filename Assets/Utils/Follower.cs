using UnityEngine;

namespace Assets.Utils
{
    public class Follower : MonoBehaviour
    {
        public Transform Target;

        private void Start()
        {
        }

        private void Update()
        {
            // Can't follow something that doesn't exist
            if (Target == null)
            {
                return;
            }

            transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
        }
    }
}