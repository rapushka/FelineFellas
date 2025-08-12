using Entitas.Generic;

namespace FelineFellas
{
    /// both Player & Enemy
    public sealed class Actor : FlagComponent, IInScope<GameScope> { }

    public sealed class ActiveActor : FlagComponent, IInScope<GameScope> { }

    public sealed class PlayerActor : FlagComponent, IInScope<GameScope>, IUnique { }

    public sealed class ActiveEnemyActor : FlagComponent, IInScope<GameScope>, IUnique { }

    /// Enemy Actor -> Stages
    public sealed class EnemyActorOnStage : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class EnemyLoadout : ValueComponent<LoadoutConfig>, IInScope<GameScope> { }

    /// Enemy Lead -> Stage
    public sealed class EnemyLeadOnMap : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class HandSize : ValueComponent<int>, IInScope<GameScope> { }

    /// Actor -> Deck
    public sealed class OwnedDeck : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class HasFullHand : FlagComponent, IInScope<GameScope> { }

    public sealed class DrawingCards : FlagComponent, IInScope<GameScope> { }

    public sealed class WaitingForDeckShuffle : FlagComponent, IInScope<GameScope> { }
}