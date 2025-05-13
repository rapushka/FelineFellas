using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class UnitCardConfig
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float Strength    { get; private set; }
    }
}