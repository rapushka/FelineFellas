using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Map : FlagComponent, IInScope<GameScope> { }

    /// Yet, it won't be added from anywhere
    public sealed class StartFight : FlagComponent, IInScope<GameScope> { }
}