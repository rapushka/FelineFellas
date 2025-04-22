using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class FieldConfig
    {
        [field: SerializeField] public Size FieldSize { get; private set; }
    }
}