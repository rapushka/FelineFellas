using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class MoneyConfig
    {
        [field: SerializeField] public int MoneyOnStart { get; private set; }
    }
}