using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class TargetPosition : ValueComponent<Vector2>, IInScope<GameScope> { }

    public sealed class AnimationsSpeed : ValueComponent<float>, IInScope<GameScope> { }
}