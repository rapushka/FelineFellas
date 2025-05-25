using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseLimitReached : FlagComponent, IInScope<GameScope>, IEvent<Self> { }
}