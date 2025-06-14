using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Field : FlagComponent, IInScope<GameScope> { }

    public sealed class FieldBorders : ValueComponent<Borders>, IInScope<GameScope> { } // TODO: REMOVE?

    public sealed class Cell : FlagComponent, IInScope<GameScope> { }

    public sealed class Empty : FlagComponent, IInScope<GameScope> { }

    public sealed class CellIndex : IndexComponent<int>, IInScope<GameScope> { }

    public sealed class Row : FlagComponent, IInScope<GameScope> { }

    public sealed class PlayerRow : FlagComponent, IInScope<GameScope> { }

    public sealed class EnemyRow : FlagComponent, IInScope<GameScope> { }

    // Cell -> Card
    // ShopSlot -> Card
    public sealed class PlacedCard : ValueComponent<EntityID>, IInScope<GameScope> { }

    // if Card's on field then its parent is Cell
    public sealed class OnField : FlagComponent, IInScope<GameScope> { }
}