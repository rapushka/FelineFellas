using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public struct Size
    {
        [field: SerializeField] public int Width  { get; private set; }
        [field: SerializeField] public int Height { get; private set; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString() => $"{Width}x{Height}";
    }
}