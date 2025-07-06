using Entitas.Generic;

namespace FelineFellas
{
    public sealed class OutOfStamina : FlagComponent, IInScope<GameScope>, IEvent<Self> { }
}