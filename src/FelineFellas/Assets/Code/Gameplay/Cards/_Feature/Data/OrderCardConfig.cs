using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class OrderCardConfig
    {
        [field: SerializeField] public AllowedTargetSubjectType TargetSubject { get; private set; }
        [field: SerializeField] public AbilityConfig            Ability       { get; private set; }

        [Flags]
        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum AllowedTargetSubjectType
        {
            Unknown = 0,
            Fella = 1 << 0,
            Lead = 1 << 1,
            Enemy = 1 << 2,
        }
    }
}