using Assets.Utils;
using UnityEngine;

namespace Assets.Player
{
    [RequireComponent(typeof (AirplaneController))]
    public class PlayerMoveInput : MonoBehaviour
    {
        private AirplaneController _controller;

        public Camera AimInputCamera;

        private void Start()
        {
            _controller = gameObject.GetComponent<AirplaneController>();
        }

        private void Update()
        {
            // Get the target position
            var directionVector = new Vector3(
                Input.mousePosition.x - AimInputCamera.pixelWidth/2.0f,
                Input.mousePosition.y - AimInputCamera.pixelHeight/2.0f, 0);
            _controller.TargetPosition = transform.position + directionVector;
        }
    }
}