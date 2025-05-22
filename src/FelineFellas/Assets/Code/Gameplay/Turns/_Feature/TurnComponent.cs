using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DelayBeforeStartTurn : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class StartPlayerTurnEvent : FlagComponent, IInScope<GameScope> { }

    public sealed class EndPlayerTurnEvent : FlagComponent, IInScope<GameScope> { }
}