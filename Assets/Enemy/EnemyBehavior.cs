using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        private GameObject _target;

        /// The direction we're currently moving in euler angles (when not chasing a _target)
        private float _currentAngle;

        private float _rotateTimer;
        private bool _chaseTarget;
        private bool _isChasingTarget;

        public float MoveSpeed = 1.5f;
        public float ChaseDistance = 2;

        // Use this for initialization
        private void Start()
        {
            // Find target
            _target = GameObject.Find("Player");

            LookNearTarget();

            _currentAngle = transform.rotation.eulerAngles.z;

            if (Random.Range(0, 3) == 0)
                _chaseTarget = false;

            // Set random rotate timer
            _rotateTimer = Random.Range(1F, 4F);
        }

        // Update is called once per frame
        private void Update()
        {
            // Calculate distance to target
            var distance = _target != null ? Vector3.Distance(transform.position, _target.transform.position) : 9999f;

            if (Mathf.Abs(distance) >= ChaseDistance) // Too far away to chase
            {
                _isChasingTarget = false;
                _currentAngle = transform.rotation.eulerAngles.z;

                _rotateTimer -= Time.deltaTime;

                if (_rotateTimer <= 0)
                {
                    // Randomly rotate
                    _currentAngle += -45 + Random.Range(0, 90);

                    // Set new random rotate timer
                    _rotateTimer = Random.Range(1F, 4F);
                }

                if (Math.Abs(transform.rotation.eulerAngles.z - _currentAngle) > 2F)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation,
                        Quaternion.Euler(0, 0, _currentAngle + 90), 10F*Time.deltaTime);
                }
            }
            else // Chase target
            {
                _isChasingTarget = true;
                LookAtTarget();
            }

            Move();
        }

        private void LookAtTarget()
        {
            if (_target == null)
            {
                return;
            }

            var difference = _target.transform.position - transform.position;
            difference.Normalize();

            var rotation = Mathf.Atan2(difference.y, difference.x)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0F, 0F, rotation - 90);
        }

        private void LookNearTarget()
        {
            LookAtTarget();
            // Look near _target
            transform.Rotate(0, 0, -20 + Random.Range(0, 40));
        }

        private void Move()
        {
            if (_isChasingTarget)
                transform.position = Vector3.MoveTowards(
                    transform.position, _target.transform.position,
                    MoveSpeed*Time.deltaTime);
            else
            {
                transform.position += transform.up*MoveSpeed*Time.deltaTime;
            }
        }
    }
}

