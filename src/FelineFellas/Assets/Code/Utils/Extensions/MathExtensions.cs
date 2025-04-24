using UnityEngine;

namespace FelineFellas
{
    public static class MathExtensions
    {
        public static float Clamp(this float @this, float? min = null, float? max = null)
            => Mathf.Clamp(@this, min ?? @this, max ?? @this);

        public static float ToRadians(this float degrees) => degrees * Mathf.Deg2Rad;

        public static float ToDegrees(this float radians) => radians * Mathf.Rad2Deg;
    }
}