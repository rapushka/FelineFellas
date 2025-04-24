using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class PlayerInput : FlagComponent, IInScope<InputScope> { }

    public sealed class Collider : ValueComponent<Collider2D>, IInScope<GameScope> { }
}