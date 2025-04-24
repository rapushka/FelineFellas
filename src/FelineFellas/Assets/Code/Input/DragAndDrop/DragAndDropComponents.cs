using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Draggable : FlagComponent, IInScope<GameScope> { }

    public sealed class Dragging : FlagComponent, IInScope<GameScope> { }

    public sealed class Dropped : FlagComponent, IInScope<GameScope> { }
}