using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Stage : PrimaryIndexComponent<StageID>, IInScope<GameScope> { }

    /// just a "mock" stage, to put player in. won't be destroyed
    public sealed class PlayerStage : FlagComponent, IInScope<GameScope> { }

    public sealed class EnteringStage : FlagComponent, IInScope<GameScope> { }

    public sealed class CompletedStage : FlagComponent, IInScope<GameScope> { }

    public sealed class StageCompletedEvent : FlagComponent, IInScope<GameScope> { }

    public sealed class ArrangeStagesEvent : FlagComponent, IInScope<GameScope> { }
}