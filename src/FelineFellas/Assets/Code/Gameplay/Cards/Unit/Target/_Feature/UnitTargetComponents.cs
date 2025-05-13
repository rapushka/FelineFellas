using Entitas.Generic;

namespace FelineFellas
{
    public sealed class FindClosestUnitRequest : FlagComponent, IInScope<GameScope> { }

    public sealed class FindClosestOpponentRequest : FlagComponent, IInScope<GameScope> { }
}