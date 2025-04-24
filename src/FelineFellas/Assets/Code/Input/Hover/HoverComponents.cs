using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Interactable : FlagComponent, IInScope<GameScope> { }

    public sealed class Hovered : FlagComponent, IInScope<GameScope>, IEvent<Self> { }
}