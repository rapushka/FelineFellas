using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Stage : FlagComponent, IInScope<GameScope> { }

    public sealed class EnteringStage : FlagComponent, IInScope<GameScope> { }
}