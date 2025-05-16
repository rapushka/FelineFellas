using UnityEngine;

namespace FelineFellas
{
    public static class VectorExtensions
    {
        public static float DistanceTo(this Vector2 @this, Vector2 other)
            => Vector2.Distance(@this, other);

        public static Vector2 Add(this Vector2 @this, float? x = null, float? y = null)
            => new(
                x: @this.x + x ?? 0,
                y: @this.y + y ?? 0
            );

        public static Vector2 Set(this Vector2 @this, float? x = null, float? y = null)
            => new(
                x: x ?? @this.x,
                y: y ?? @this.y
            );
    }
}