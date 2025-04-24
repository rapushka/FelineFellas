using Entitas.Generic;

namespace FelineFellas
{
    /// A Card without specific target, used simply on the game field
    public sealed class GlobalCard : FlagComponent, IInScope<GameScope> { }

    public sealed class WillBeUsed : FlagComponent, IInScope<GameScope> { }
}