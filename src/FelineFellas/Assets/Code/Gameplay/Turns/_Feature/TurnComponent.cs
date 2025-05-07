using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StartTurnEvent : FlagComponent, IInScope<GameScope> { }

    public sealed class EndTurnEvent : FlagComponent, IInScope<GameScope> { }
}