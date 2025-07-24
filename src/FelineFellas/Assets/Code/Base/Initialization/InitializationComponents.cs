using Entitas.Generic;

namespace FelineFellas
{
    /// Lives for the first frame after entity creation
    public sealed class Initializing : FlagComponent, IInScope<GameScope> { }
}