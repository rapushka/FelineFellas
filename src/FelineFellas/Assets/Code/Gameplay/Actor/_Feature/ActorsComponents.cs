using Entitas.Generic;

namespace FelineFellas
{
    /// both Player & Enemy
    public sealed class Actor : FlagComponent, IInScope<GameScope> { }

    public sealed class ActiveActor : FlagComponent, IInScope<GameScope> { }

    public sealed class PlayerActor : FlagComponent, IInScope<GameScope>, IUnique { }

    public sealed class EnemyActor : FlagComponent, IInScope<GameScope> { }

    public sealed class ActorOnStage : PrimaryIndexComponent<StageID>, IInScope<GameScope> { }

    public sealed class LeadOnStage : PrimaryIndexComponent<StageID>, IInScope<GameScope> { }

    public sealed class DeckOnStage : PrimaryIndexComponent<StageID>, IInScope<GameScope> { }

    public sealed class EnemyLoadout : ValueComponent<LoadoutConfig>, IInScope<GameScope> { }

    public sealed class HandSize : ValueComponent<int>, IInScope<GameScope> { }

    public sealed class HasFullHand : FlagComponent, IInScope<GameScope> { }

    public sealed class DrawingCards : FlagComponent, IInScope<GameScope> { }

    public sealed class WaitingForDeckShuffle : FlagComponent, IInScope<GameScope> { }
}