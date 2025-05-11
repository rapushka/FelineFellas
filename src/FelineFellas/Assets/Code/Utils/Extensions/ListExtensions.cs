using System.Collections.Generic;
using UnityEngine;

namespace FelineFellas
{
    public static class ListExtensions
    {
        public static void DestroyAllObjects<T>(this List<T> @this)
            where T : MonoBehaviour
        {
            foreach (var monoBehaviour in @this)
                monoBehaviour.DestroyObject();

            @this.Clear();
        }
    }
}