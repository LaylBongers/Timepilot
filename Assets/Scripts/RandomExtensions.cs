using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    internal static class RandomExtensions
    {
        public static float NextFloat(this Random random, float max)
        {
            return (float) random.NextDouble()*max;
        }

        public static Vector3 NextVector(this Random random, Vector3 origin, float distance)
        {
            var doubleDistance = distance*2;
            return origin + new Vector3(
                random.NextFloat(doubleDistance) - distance,
                random.NextFloat(doubleDistance) - distance,
                random.NextFloat(doubleDistance) - distance);
        }
    }
}