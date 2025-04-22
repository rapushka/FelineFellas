using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class View : ValueComponent<GameEntityBehaviour>, IInScope<GameScope> { }

    public sealed class WorldPosition : ValueComponent<Vector2>, IInScope<GameScope>, IEvent<Self> { }
}