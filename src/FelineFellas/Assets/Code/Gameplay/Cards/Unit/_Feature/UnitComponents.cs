using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UnitCard : FlagComponent, IInScope<GameScope> { }

    public sealed class Leader : FlagComponent, IInScope<GameScope> { }

    public sealed class Enemy : FlagComponent, IInScope<GameScope> { }

    public sealed class MaxHealth : ValueComponent<float>, IInScope<GameScope>, IEvent<Self> { }

    public sealed class Health : ValueComponent<float>, IInScope<GameScope>, IEvent<Self> { }
}