using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class EventCardConfig
    {
        [field: SerializeField] public AbilityConfig Ability  { get; private set; }
        [field: SerializeField] public bool          IsGlobal { get; private set; }
    }
}