using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Field : FlagComponent, IInScope<GameScope> { }

    public sealed class Cell : FlagComponent, IInScope<GameScope> { }

    public sealed class Empty : FlagComponent, IInScope<GameScope> { }

    public sealed class CellCoordinates : PrimaryIndexComponent<Coordinates>, IInScope<GameScope> { }

    public sealed class PlacedUnit : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class OnField : PrimaryIndexComponent<Coordinates>, IInScope<GameScope> { }
}