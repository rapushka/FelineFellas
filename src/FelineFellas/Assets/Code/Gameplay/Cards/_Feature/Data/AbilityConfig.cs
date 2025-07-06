using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class AbilityConfig
    {
        [field: SerializeField] public AbilityTypeID TypeID { get; private set; }

        [field: NaughtyAttributes.AllowNesting]
        [field: SerializeField] public TargetObjectTypeID TargetObject { get; private set; }

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
            => TypeID is AbilityTypeID.Attack;

        private bool HasDirection()
            => TargetObject is TargetObjectTypeID.FreeCell;

        private bool HasBelonging()
            => TargetObject is TargetObjectTypeID.FreeCell;

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