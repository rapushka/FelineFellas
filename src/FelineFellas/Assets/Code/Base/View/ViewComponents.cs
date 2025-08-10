using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class View : ValueComponent<GameEntityBehaviour>, IInScope<GameScope> { }

    public sealed class WorldPosition : ValueComponent<Vector2>, IInScope<GameScope>, IInScope<InputScope>, IEvent<Self> { }

    public sealed class ScreenPosition : ValueComponent<Vector2>, IInScope<InputScope> { }

    public sealed class Visible : ValueComponent<bool>, IInScope<GameScope>, IEvent<Self> { }
}