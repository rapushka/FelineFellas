using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class TurnsConfig
    {
        [field: SerializeField] public ViewConfig View { get; private set; }

        [Serializable]
        public class ViewConfig
        {
            [field: SerializeField] public float EnemyTurnDuration { get; private set; } = 1f;
        }
    }
}