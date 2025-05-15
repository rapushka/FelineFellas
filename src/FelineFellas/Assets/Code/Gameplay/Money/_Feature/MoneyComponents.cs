using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Price : ValueComponent<int>, IInScope<GameScope>, IEvent<Self> { }

    public sealed class Money : ValueComponent<int>, IInScope<GameScope>, IEvent<Self> { }
}