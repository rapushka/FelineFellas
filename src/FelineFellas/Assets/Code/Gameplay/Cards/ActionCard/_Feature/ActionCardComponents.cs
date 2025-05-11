using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ActionCard : FlagComponent, IInScope<GameScope> { }

    public sealed class ActionMove : FlagComponent, IInScope<GameScope> { }

    public sealed class TargetSelectNeighbor : ValueComponent<Coordinates>, IInScope<GameScope> { }

    public sealed class ActionValue : ValueComponent<float>, IInScope<GameScope> { }
}