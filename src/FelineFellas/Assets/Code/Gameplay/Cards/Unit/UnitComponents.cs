using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UnitCard : FlagComponent, IInScope<GameScope> { }

    public sealed class Leader : FlagComponent, IInScope<GameScope> { }
}