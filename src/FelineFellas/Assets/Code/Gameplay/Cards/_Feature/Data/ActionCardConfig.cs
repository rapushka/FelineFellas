using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class ActionCardConfig
    {
        [field: SerializeField] public ActionCardType ActionType { get; private set; }
        [field: SerializeField] public float          Value      { get; private set; }

        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum ActionCardType
        {
            Unknown = 0,
            MoveUp = 1,
        }
    }
}