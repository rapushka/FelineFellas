using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ActionCard : FlagComponent, IInScope<GameScope> { }

    public sealed class TargetSelectNeighbor : ValueComponent<Coordinates>, IInScope<GameScope> { }

    public sealed class TargetSelectClosestOpponent : FlagComponent, IInScope<GameScope> { }

    public sealed class SelectedTarget : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class ActionValue : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class CanUseOnFella : FlagComponent, IInScope<GameScope> { }

    public sealed class CanUseOnLeader : FlagComponent, IInScope<GameScope> { }

    public sealed class CanUseOnEnemy : FlagComponent, IInScope<GameScope> { }
}