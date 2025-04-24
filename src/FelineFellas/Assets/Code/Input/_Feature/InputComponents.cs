using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class PlayerInput : FlagComponent, IInScope<InputScope> { }

    public sealed class Collider : ValueComponent<Collider2D>, IInScope<GameScope> { }

    public sealed class CursorJustDown : FlagComponent, IInScope<InputScope> { }

    public sealed class CursorJustClicked : FlagComponent, IInScope<InputScope> { }
    public sealed class CursorHolding : FlagComponent, IInScope<InputScope> { }
}