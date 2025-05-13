using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UnitCard : FlagComponent, IInScope<GameScope> { }

    public sealed class Fella : FlagComponent, IInScope<GameScope> { }

    public sealed class Leader : FlagComponent, IInScope<GameScope> { }

    public sealed class Enemy : FlagComponent, IInScope<GameScope> { }

    public sealed class MaxHealth : ValueComponent<int>, IInScope<GameScope>, IEvent<Self> { }

    public sealed class Health : ValueComponent<int>, IInScope<GameScope>, IEvent<Self> { }

    public sealed class Strength : ValueComponent<int>, IInScope<GameScope>, IEvent<Self> { }
}