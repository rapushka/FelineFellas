using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Deck : FlagComponent, IInScope<GameScope> { }

    public sealed class NeedsShuffle : FlagComponent, IInScope<GameScope> { }

    public sealed class ShufflingDeckTimer : ValueComponent<float>, IInScope<GameScope> { }

    /// Includes waiting for deck shuffle
    public sealed class DrawingCards : FlagComponent, IInScope<GameScope> { }

    /// Only the moment to draw the cards rn
    public sealed class DrawCardsEvent : FlagComponent, IInScope<GameScope> { }

    public sealed class CardInDeck : IndexComponent<EntityID>, IInScope<GameScope> { }
}