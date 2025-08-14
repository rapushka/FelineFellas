using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Defeated : FlagComponent, IInScope<GameScope>, IEvent<Self> { }
}