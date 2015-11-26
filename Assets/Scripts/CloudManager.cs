using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class CloudManager : MonoBehaviour
    {
        private readonly List<GameObject> _clouds = new List<GameObject>();
        private readonly Random _random = new Random();

        public int CloudCount = 20;
        public float CloudDistance = 6;
        public GameObject CloudPrefab;
        public Transform TargetPlayer;

        private void Start()
        {
            // Spawn in a bunch of clouds initially
            for (var i = 0; i < CloudCount; i++)
            {
                var obj = Instantiate(CloudPrefab);
                obj.transform.position = _random.NextVector(TargetPlayer.position, CloudDistance);
                obj.transform.parent = gameObject.transform;
                _clouds.Add(obj);
            }
        }

        private void Update()
        {
            // Trim clouds too far away
            for (var i = 0; i < _clouds.Count; i++)
            {
                var ourPos = TargetPlayer.position;
                var pos = _clouds[i].transform.position;

                if (Math.Abs(ourPos.x - pos.x) <= CloudDistance && Math.Abs(ourPos.y - pos.y) <= CloudDistance)
                {
                    // It's fine, leave it
                    continue;
                }

                // It's too far away, remove it
                Destroy(_clouds[i]);
                _clouds.RemoveAt(i);
                i--;
            }

            // Add new clouds if we have to
            while (_clouds.Count < CloudCount)
            {
                // Make sure the cloud's at one of the edges
                var otherPos = TargetPlayer.position;
                var pos = _random.NextVector(otherPos, CloudDistance);
                var axis = _random.Next(2);
                pos[axis] = _random.Next(2) == 1 ? otherPos[axis] + CloudDistance : otherPos[axis] - CloudDistance;

                // Create and add the cloud
                var obj = Instantiate(CloudPrefab);
                obj.transform.position = pos;
                obj.transform.parent = gameObject.transform;
                _clouds.Add(obj);
            }
        }
    }
}