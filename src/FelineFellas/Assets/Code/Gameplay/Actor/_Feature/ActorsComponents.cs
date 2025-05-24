using Entitas.Generic;

namespace FelineFellas
{
    /// both Player & Enemy
    public sealed class Actor : FlagComponent, IInScope<GameScope> { }

    public sealed class PlayerActor : FlagComponent, IInScope<GameScope>, IUnique { }

    public sealed class EnemyActor : FlagComponent, IInScope<GameScope>, IUnique { }

    public sealed class HandSize : ValueComponent<int>, IInScope<GameScope> { }

    /// Actor -> Deck
    public sealed class OwnedDeck : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class HasFullHand : FlagComponent, IInScope<GameScope> { }

    public sealed class DrawingCardsActor : FlagComponent, IInScope<GameScope> { } // TODO: RENAME TO SIMPLY DrawingCards

    public sealed class WaitingForDeckShuffle : FlagComponent, IInScope<GameScope> { }
}