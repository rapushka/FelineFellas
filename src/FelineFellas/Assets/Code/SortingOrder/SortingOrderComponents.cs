using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SpriteSortingGroup : ValueComponent<SortGroup>, IInScope<GameScope> { }

    public sealed class SpriteSortingIndex : ValueComponent<int>, IInScope<GameScope>, IEvent<Self> { }
}