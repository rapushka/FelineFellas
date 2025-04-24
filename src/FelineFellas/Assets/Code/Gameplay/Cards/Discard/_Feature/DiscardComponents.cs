using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SendToDiscard : FlagComponent, IInScope<GameScope> { }
    public sealed class InDiscard : FlagComponent, IInScope<GameScope> { }
}