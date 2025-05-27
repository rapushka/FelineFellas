using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DelayBeforePlayingCard : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class TryPlayCard : FlagComponent, IInScope<GameScope> { }

    /// Actor -> Card
    public sealed class CardToPlay : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class WillPlayActionCard : FlagComponent, IInScope<GameScope> { }

    public sealed class Priority : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class CanNotPlay : FlagComponent, IInScope<GameScope> { }
}