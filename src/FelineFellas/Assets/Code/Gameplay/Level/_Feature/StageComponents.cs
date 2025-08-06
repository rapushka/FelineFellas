using Entitas.Generic;

namespace FelineFellas
{
    /// value = level number
    public sealed class Stage : ValueComponent<int>, IInScope<GameScope> { }

    public sealed class EnteringStage : FlagComponent, IInScope<GameScope> { }
}