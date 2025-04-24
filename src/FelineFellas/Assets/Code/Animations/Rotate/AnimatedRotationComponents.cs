using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Rotation : ValueComponent<float>, IInScope<GameScope>, IEvent<Self> { }

    public sealed class TargetRotation : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class RotationSpeed : ValueComponent<float>, IInScope<GameScope> { }
}