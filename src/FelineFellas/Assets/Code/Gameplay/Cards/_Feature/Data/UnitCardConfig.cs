using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class UnitCardConfig
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
    }
}