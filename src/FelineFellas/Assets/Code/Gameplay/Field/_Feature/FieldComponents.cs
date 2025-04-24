using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Field : FlagComponent, IInScope<GameScope> { }

    public sealed class Cell : FlagComponent, IInScope<GameScope> { }
}