using UnityEngine;

namespace Assets.Utils
{
    public class AirplaneController : MonoBehaviour
    {
        private Transform _innerModel;

        public Vector3 TargetPosition;
        public float Speed = 2f;
        public float RotateSpeed = 40f;

        private void Start()
        {
            _innerModel = transform.childCount == 0 ? null : transform.GetChild(0);
        }

        private void Update()
        {
            // Get the direction to rotate to
            var directionVector = TargetPosition - transform.position;
            var targetDirection = Mathf.Rad2Deg*Mathf.Atan2(directionVector.x, -directionVector.y) + 180;

            // Rotate in the direction of the target
            var currentDirection = transform.rotation.eulerAngles.z;
            var resultDirection = RotateToWithLimit(currentDirection, targetDirection, RotateSpeed*Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, resultDirection);

            // If we have an inner model, rotate that around for effect
            if (_innerModel != null)
            {
                var innerCurrent = _innerModel.localEulerAngles;
                innerCurrent.y = -resultDirection;

                if (innerCurrent.y <= -90 && innerCurrent.y >= -270)
                {
                    innerCurrent.y = -180 - innerCurrent.y;
                }

                _innerModel.localEulerAngles = innerCurrent;
                if (gameObject.name == "Player")
                {
                    Debug.Log(string.Format("{0}", innerCurrent.y));
                }
            }

            // Move in the right direction
            var direction = transform.rotation*Vector3.up;
            transform.position += direction*Time.deltaTime*Speed;
        }

        private static float RotateToWithLimit(float source, float target, float limit)
        {
            // Normalize the input
            source = DegMod(source);
            target = DegMod(target);

            // Calculate our actual difference
            var posDiff = DegMod(target - source);
            var negDiff = -DegMod(360 - posDiff);

            // Get the smallest diff
            var diff = Mathf.Abs(posDiff) < Mathf.Abs(negDiff) ? posDiff : negDiff;

            // Limit our diff so we only move at a certain speed
            var limitedDiff = diff;
            if (limitedDiff > limit)
            {
                limitedDiff = limit;
            }
            else if (limitedDiff < -limit)
            {
                limitedDiff = -limit;
            }

            return source + limitedDiff;
        }

        private static float DegMod(float value)
        {
            return (value%360f + 360f)%360f;
        }
    }
}