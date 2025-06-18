using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class AbilityConfig
    {
        [field: SerializeField] public AbilityType Type { get; private set; }

        [field: NaughtyAttributes.ShowIf(nameof(HasTargetObject))]
        [field: NaughtyAttributes.AllowNesting]
        [field: SerializeField] public TargetObjectSelectionType TargetObject { get; private set; }

        [field: NaughtyAttributes.ShowIf(nameof(HasValue))]
        [field: NaughtyAttributes.AllowNesting]
        [field: SerializeField] public float Value { get; private set; }

        [field: NaughtyAttributes.ShowIf(nameof(HasDirection))]
        [field: NaughtyAttributes.AllowNesting]
        [field: SerializeField] public CellDirection Direction { get; private set; }

        [field: NaughtyAttributes.ShowIf(nameof(HasBelonging))]
        [field: NaughtyAttributes.AllowNesting]
        [field: SerializeField] public Belonging TargetObjectBelonging { get; private set; }

        private bool HasValue()
            => Type is AbilityType.Attack;

        private bool HasTargetObject()
            => Type is AbilityType.Move or AbilityType.Attack;

        private bool HasDirection()
            => TargetObject is TargetObjectSelectionType.FreeCell;

        private bool HasBelonging()
            => TargetObject is TargetObjectSelectionType.FreeCell;

        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum AbilityType
        {
            Unknown = 0,
            Move = 1,
            Attack = 2,
            SendToDiscard = 3,
        }

        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum TargetObjectSelectionType
        {
            Unknown = 0,
            Opponent = 2,
            FreeCell = 3,
        }

        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum Belonging
        {
            Unknown = 0,
            Our = 1,
            Enemy = 2,
        }
    }
}