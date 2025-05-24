using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Deck : FlagComponent, IInScope<GameScope> { }

    public sealed class NeedsShuffle : FlagComponent, IInScope<GameScope> { }

    public sealed class ShufflingDeckTimer : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class CardInDeck : IndexComponent<EntityID>, IInScope<GameScope> { }
}