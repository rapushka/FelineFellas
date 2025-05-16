using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class Shop : FlagComponent, IInScope<GameScope> { }

    public sealed class SellAreaCollider : ValueComponent<Collider2D>, IInScope<GameScope> { }

    public sealed class ShopSlot : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class BuyButton : ValueComponent<Collider2D>, IInScope<GameScope> { }

    public sealed class CanBuy : FlagComponent, IInScope<GameScope> { }

    public sealed class Buy : FlagComponent, IInScope<GameScope> { }

    public sealed class CardInShop : FlagComponent, IInScope<GameScope> { }
}