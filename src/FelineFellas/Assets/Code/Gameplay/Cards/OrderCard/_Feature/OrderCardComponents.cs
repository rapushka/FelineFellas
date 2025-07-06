using Entitas.Generic;

namespace FelineFellas
{
    public sealed class OrderCard : FlagComponent, IInScope<GameScope> { }

    // As Target Subject. Used only for Order Cards

    public sealed class CanTargetSubjectFella : FlagComponent, IInScope<GameScope> { }

    public sealed class CanTargetSubjectLeader : FlagComponent, IInScope<GameScope> { }

    public sealed class CanTargetSubjectEnemy : FlagComponent, IInScope<GameScope> { }
}