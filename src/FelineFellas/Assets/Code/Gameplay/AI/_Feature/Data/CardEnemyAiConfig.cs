using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class CardEnemyAiConfig
    {
        [field: SerializeField] public float Priority { get; private set; }
    }
}