using UnityEngine;

namespace FelineFellas
{
    public static class GameObjectExtensions
    {
        public static void DestroyObject(this Transform @this)
        {
            if (@this != null)
                @this.gameObject.DestroyObject();
        }

        public static void DestroyObject(this MonoBehaviour @this)
        {
            if (@this != null)
                @this.gameObject.DestroyObject();
        }

        public static void DestroyObject(this GameObject @this)
        {
            if (@this == null)
                return;

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Object.DestroyImmediate(@this);
                return;
            }
#endif

            Object.Destroy(@this);
        }
    }
}