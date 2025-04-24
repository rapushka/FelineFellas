using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Scale : ValueComponent<float>, IInScope<GameScope>, IEvent<Self> { }

    public sealed class TargetScale : ValueComponent<float>, IInScope<GameScope>, IEvent<Self> { }
}