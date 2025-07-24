using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class MapConfig
    {
        [field: SerializeField] public int NumberOfUsualEnemies { get; private set; }

        [field: SerializeField] public ViewConfig View { get; private set; }

        [Serializable]
        public class ViewConfig
        {
            [field: SerializeField] public float Space { get; private set; }
        }
    }
}