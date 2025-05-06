using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class UiConfig
    {
        [field: SerializeField] public Canvas CanvasPrefab { get; private set; }
    }
}