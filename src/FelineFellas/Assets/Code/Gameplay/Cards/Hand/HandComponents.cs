using Entitas.Generic;

namespace FelineFellas
{
    public sealed class InHandIndex : ValueComponent<int>, IInScope<GameScope> { }

    public sealed class RecalculateInHandIndexes : FlagComponent, IInScope<GameScope> { }
}