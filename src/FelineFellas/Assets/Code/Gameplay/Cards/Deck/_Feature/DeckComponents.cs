using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DrawCardsEvent : FlagComponent, IInScope<GameScope> { }

    public sealed class InDeck : FlagComponent, IInScope<GameScope> { }
}