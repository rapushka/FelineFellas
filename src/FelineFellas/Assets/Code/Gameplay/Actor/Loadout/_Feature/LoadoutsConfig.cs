using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class LoadoutsConfig
    {
        [field: SerializeField] public LoadoutConfig PlayerLoadout { get; private set; }

        [field: SerializeField] public LoadoutConfig[] EnemyLoadouts { get; private set; }
    }
}