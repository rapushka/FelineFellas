using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class Shop : FlagComponent, IInScope<GameScope> { }

    public sealed class SellArea : ValueComponent<Collider2D>, IInScope<GameScope> { }
}