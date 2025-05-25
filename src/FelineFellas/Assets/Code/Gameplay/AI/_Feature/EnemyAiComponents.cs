using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Priority : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class CanNotPlay : FlagComponent, IInScope<GameScope> { }
}