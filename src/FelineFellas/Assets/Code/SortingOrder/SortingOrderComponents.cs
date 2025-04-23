using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Sorting : ValueComponent<SortGroup>, IInScope<GameScope>, IEvent<Self> { }
}