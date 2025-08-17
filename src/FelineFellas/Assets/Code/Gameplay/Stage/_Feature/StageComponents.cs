using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Stage : ValueComponent<StageID>, IInScope<GameScope> { }

    public sealed class EnteringStage : FlagComponent, IInScope<GameScope> { }

    public sealed class CompletedStage : FlagComponent, IInScope<GameScope> { }

    public sealed class StageCompletedEvent : FlagComponent, IInScope<GameScope> { }
}