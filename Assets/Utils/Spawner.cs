using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Utils
{
    public class Spawner : MonoBehaviour
    {
        private readonly List<GameObject> _instances = new List<GameObject>();
        private Random _random;

        public int Count = 10;
        public float Distance = 6;
        public Transform TargetCamera;
        public float MinimalInstanceDistance = 0;

        public GameObject Prefab;
        public int SeedAddition;
        public bool SpawnInitial = false;

        private void Start()
        {
            // Initialize our rng
            _random = new Random(Environment.TickCount + SeedAddition);

            if (SpawnInitial)
            {
                // Spawn in a bunch of instances initially
                for (var i = 0; i < Count; i++)
                {
                    var obj = Instantiate(Prefab);
                    obj.transform.position = _random.NextVector2D(TargetCamera.position.Xyn(0), Distance);
                    obj.transform.parent = gameObject.transform;
                    _instances.Add(obj);
                }
            }
        }

        private void Update()
        {
            // Trim any unneeded instances
            for (var i = 0; i < _instances.Count; i++)
            {
                // Check if we can keep this instance
                var instance = _instances[i];
                if (instance != null && InstanceIsInRange(instance))
                {
                    continue;
                }

                // We can't, remove it
                Destroy(_instances[i]);
                _instances.RemoveAt(i);
                i--;
            }

            // Add new instances if we have to
            while (_instances.Count < Count)
            {
                // Make sure the cloud's at one of the edges
                var otherPos = TargetCamera.position;
                var pos = _random.NextVector2D(otherPos.Xyn(0), Distance);
                var axis = _random.Next(2);
                pos[axis] = _random.Next(2) == 1 ? otherPos[axis] + Distance : otherPos[axis] - Distance;

                // Create and add the cloud
                var obj = Instantiate(Prefab);
                obj.transform.position = pos;
                obj.transform.parent = gameObject.transform;
                _instances.Add(obj);
            }
        }

        private bool InstanceIsInRange(GameObject instance)
        {
            var camPos = TargetCamera.position;
            var pos = instance.transform.position;

            return Math.Abs(camPos.x - pos.x) <= Distance && Math.Abs(camPos.y - pos.y) <= Distance;
        }

        private void OnDrawGizmosSelected()
        {
            if (TargetCamera == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(TargetCamera.position, new Vector3(Distance*2, Distance*2, 0));
        }
    }
}