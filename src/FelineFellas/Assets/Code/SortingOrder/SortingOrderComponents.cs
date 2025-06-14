using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SpriteSortingGroup : ValueComponent<RenderOrder>, IInScope<GameScope> { }

    public sealed class RenderOrderIndex : ValueComponent<int>, IInScope<GameScope>, IEvent<Self> { }
}