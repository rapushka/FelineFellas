using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Interactable : FlagComponent, IInScope<GameScope>, IEvent<Self> { }

    public sealed class Hovered : FlagComponent, IInScope<GameScope>, IEvent<Self> { }
}