using UnityEngine;

namespace Assets.Scripts
{
    internal static class SwizzlingExtensions
    {
        public static Vector3 Xyn(this Vector3 value, float z)
        {
            value.z = z;
            return value;
        }

        public static Vector2 Xy(this Vector3 value)
        {
            return new Vector2(value.x, value.y);
        }
    }
}