using System;

namespace FelineFellas
{
    [Serializable]
    [JetBrains.Annotations.UsedImplicitly]
    public enum TargetObjectTypeID
    {
        Unknown = 0,
        Opponent = 2,
        FreeCell = 3,
        Self = 4,
    }
}