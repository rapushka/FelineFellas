using Entitas.Generic;

namespace FelineFellas
{
    /// both Player & Enemy
    public sealed class Actor : FlagComponent, IInScope<GameScope> { }

    public sealed class ActiveActor : FlagComponent, IInScope<GameScope> { }

    public sealed class PlayerActor : FlagComponent, IInScope<GameScope>, IUnique { }

    /// Enemy Actor -> Stage
    public sealed class EnemyActor : ValueComponent<EntityID>, IInScope<GameScope> { } // TODO: to flag

    public sealed class EnemyLoadout : ValueComponent<LoadoutConfig>, IInScope<GameScope> { }

    /// Enemy Lead -> Stage
    public sealed class EnemyLeadOnStage : ValueComponent<EntityID>, IInScope<GameScope> { } // TODO: REMOVE

    public sealed class HandSize : ValueComponent<int>, IInScope<GameScope> { }

    /// Actor -> Deck
    public sealed class OwnedDeck : ValueComponent<EntityID>, IInScope<GameScope> { } // TODO: REMOVE

    public sealed class HasFullHand : FlagComponent, IInScope<GameScope> { }

    public sealed class DrawingCards : FlagComponent, IInScope<GameScope> { }

    public sealed class WaitingForDeckShuffle : FlagComponent, IInScope<GameScope> { }
}