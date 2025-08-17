using Entitas.Generic;

namespace FelineFellas
{
    /// value = level number (i.e. starts with 1)
    public sealed class Stage : ValueComponent<int>, IInScope<GameScope> { }

    public sealed class EnteringStage : FlagComponent, IInScope<GameScope> { }

    public sealed class CompletedStage : FlagComponent, IInScope<GameScope> { }

    public sealed class StageCompletedEvent : FlagComponent, IInScope<GameScope> { }
}