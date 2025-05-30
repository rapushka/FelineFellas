using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class Shop : FlagComponent, IInScope<GameScope> { }

    public sealed class SellAreaCollider : ValueComponent<Collider2D>, IInScope<GameScope> { }

    public sealed class ShopSlot : ValueComponent<EntityID>, IInScope<GameScope> { }

    /// on Slot
    public sealed class BuyButton : ValueComponent<Collider2D>, IInScope<GameScope> { }

    /// on Slot
    public sealed class CanBuy : FlagComponent, IInScope<GameScope> { }

    /// on Slot
    public sealed class Buy : FlagComponent, IInScope<GameScope> { }

    /// Card -> Slot
    public sealed class CardInShopSlot : ValueComponent<EntityID>, IInScope<GameScope> { }
}