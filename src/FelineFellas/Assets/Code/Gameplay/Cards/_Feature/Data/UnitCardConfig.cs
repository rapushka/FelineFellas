using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class UnitCardConfig
    {
        [field: SerializeField] public bool IsLeader  { get; private set; }
        [field: SerializeField] public int  MaxHealth { get; private set; }
        [field: SerializeField] public int  Strength  { get; private set; }
    }
}