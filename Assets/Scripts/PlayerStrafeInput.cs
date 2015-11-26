using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerStrafeInput : MonoBehaviour
    {
        public float Distance = 1f;

        private void Update()
        {
            var directionVec = new Vector3();
            if (Input.GetKeyDown(KeyCode.A))
            {
                directionVec.y += 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                directionVec.y -= 1;
            }

            // Move in the direction of the movement vector
            var direction = transform.rotation * directionVec;
            transform.position += direction * Distance;
        }
    }
}