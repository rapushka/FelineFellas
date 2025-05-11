using Entitas.Generic;

namespace FelineFellas
{
    /// A Card without specific target, used simply on the game field
    public sealed class GlobalCard : FlagComponent, IInScope<GameScope> { }

    /// Those Cards, that will be discarded after use
    public sealed class OneShotCard : FlagComponent, IInScope<GameScope> { }

    public sealed class WillBeUsed : FlagComponent, IInScope<GameScope> { }

    public sealed class UseTarget : ValueComponent<EntityID>, IInScope<GameScope> { }
}