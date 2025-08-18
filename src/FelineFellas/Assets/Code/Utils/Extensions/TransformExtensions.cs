using UnityEngine;

namespace FelineFellas
{
    public static class TransformExtensions
    {
        public static void SetEulerAngles(this Transform @this, float? x = null, float? y = null, float? z = null)
        {
            var oldValue = @this.localEulerAngles;
            @this.localEulerAngles = new(
                x: x ?? oldValue.x,
                y: y ?? oldValue.y,
                z: z ?? oldValue.z
            );
        }

        public static Transform SetScale(this Transform @this, float value)
        {
            @this.localScale = Vector3.one * value;
            return @this;
        }
    }
}