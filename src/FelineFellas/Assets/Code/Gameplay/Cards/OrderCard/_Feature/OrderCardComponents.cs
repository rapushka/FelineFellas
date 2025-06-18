using Entitas.Generic;

namespace FelineFellas
{
    public sealed class OrderCard : FlagComponent, IInScope<GameScope> { }

    // As Target Subject. Used only for Order Cards

    public sealed class CanSelectFella : FlagComponent, IInScope<GameScope> { }

    public sealed class CanSelectLeader : FlagComponent, IInScope<GameScope> { }

    public sealed class CanSelectEnemy : FlagComponent, IInScope<GameScope> { }
}