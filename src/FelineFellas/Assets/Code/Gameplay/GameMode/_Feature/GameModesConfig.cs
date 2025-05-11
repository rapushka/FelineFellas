using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class GameModesConfig
    {
        [field: SerializeField] public GameModeConfig[] GameModes { get; private set; }
    }
}