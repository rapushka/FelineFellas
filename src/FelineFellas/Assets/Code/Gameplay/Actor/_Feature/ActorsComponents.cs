using Entitas.Generic;

namespace FelineFellas
{
    // both Player & Enemy
    public sealed class Actor : FlagComponent, IInScope<GameScope> { }

    public sealed class Player : FlagComponent, IInScope<GameScope>, IUnique { }

    public sealed class HandSize : ValueComponent<int>, IInScope<GameScope> { }
}