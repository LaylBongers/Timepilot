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

        [Tooltip("If set to more than 0, will retry spawning if too close to an existing instance.")]
        public float MinimalSpread = 0;

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
                var retryCount = 0;
                while (_instances.Count < Count)
                {
                    var pos = _random.NextVector2D(TargetCamera.position.Xyn(0), Distance);

                    // Make sure the instance can be spawned here
                    if (!IsValidSpawnPosition(pos))
                    {
                        retryCount++;
                        if (retryCount > 10)
                        {
                            Debug.LogWarning("Had to early-bail on spawning.");
                            break;
                        }
                        continue;
                    }

                    // Actually spawn the instance
                    var obj = Instantiate(Prefab);
                    obj.transform.position = pos;
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
            var retryCount = 0;
            while (_instances.Count < Count)
            {
                // Make sure the cloud's at one of the edges
                var otherPos = TargetCamera.position;
                var pos = _random.NextVector2D(otherPos.Xyn(0), Distance);
                var axis = _random.Next(2);
                pos[axis] = _random.Next(2) == 1 ? otherPos[axis] + Distance : otherPos[axis] - Distance;

                // Make sure the instance can be spawned here
                if (!IsValidSpawnPosition(pos))
                {
                    retryCount++;
                    if (retryCount > 10)
                    {
                        Debug.LogWarning("Had to early-bail on spawning.");
                        break;
                    }
                    continue;
                }

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

        private bool IsValidSpawnPosition(Vector3 pos)
        {
            foreach (var instance in _instances)
            {
                var otherPos = instance.transform.position;
                if (Math.Abs(otherPos.x - pos.x) <= MinimalSpread && Math.Abs(otherPos.y - pos.y) <= MinimalSpread)
                {
                    return false;
                }
            }

            return true;
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