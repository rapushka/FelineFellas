using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class Shop : FlagComponent, IInScope<GameScope> { }

    public sealed class SellAreaCollider : ValueComponent<Collider2D>, IInScope<GameScope> { }

    public sealed class ShopSlot : ValueComponent<EntityID>, IInScope<GameScope> { }
}