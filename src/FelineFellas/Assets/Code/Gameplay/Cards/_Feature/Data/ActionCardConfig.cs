using System;
using UnityEngine;
using Naughty = NaughtyAttributes;

namespace FelineFellas
{
    [Serializable]
    public class ActionCardConfig
    {
        [field: SerializeField] public ActionCardType ActionType { get; private set; }
        [field: SerializeField] public float          Value      { get; private set; }

        [field: SerializeField] public TargetSelectionType TargetSelection { get; private set; }

        [field: SerializeField] public AllowedTargetType AllowedTargets { get; private set; }

        [field: Naughty.ShowIf(nameof(TargetSelection), TargetSelectionType.Direction)]
        [field: Naughty.AllowNesting]
        [field: SerializeField] public Direction Direction { get; private set; }

        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum ActionCardType
        {
            Unknown = 0,
            Move = 1,
            Attack = 2,
            SendToDiscard = 3,
        }

        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum TargetSelectionType
        {
            Unknown = 0,
            Direction = 1,
            ClosestOpponent = 2,
        }

        [Flags]
        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum AllowedTargetType
        {
            Unknown = 0,
            Fella = 1 << 0,
            Lead = 1 << 1,
            Enemy = 1 << 2,
        }
    }
}