using Entitas.Generic;

namespace FelineFellas
{
    public static class FieldUtils
    {
        private static ScopeContext<GameScope> Context => Contexts.Instance.Get<GameScope>();

        public static Entity<GameScope> GetNeighborCell(Entity<GameScope> cell, CellDirection direction)
        {
            var side = cell.Get<OnSide>().Value;
            var index = cell.Get<CellIndex>().Value;

            var step = direction.Visit(
                onLeft: () => -1,
                onRight: () => 1,
                onAny: () => throw new("Can't get Neighbor for CellDirection.Any!")
            );
            return GetCellOrDefault(side, index + step);
        }

        public static Entity<GameScope> GetClosestFreeCell(Entity<GameScope> cell, CellDirection direction)
        {
            var side = cell.Get<OnSide>().Value;
            var index = cell.Get<CellIndex>().Value;

            return GetClosestFreeCellInDirection(side, index, direction);
        }

        private static Entity<GameScope> GetClosestFreeCellInDirection(Side side, int startIndex, CellDirection direction)
        {
            var tryLeft = direction is CellDirection.Left or CellDirection.Any;
            var tryRight = direction is CellDirection.Right or CellDirection.Any;

            for (var distance = 1; distance < int.MaxValue; distance++)
            {
                Entity<GameScope> leftCell = null;
                Entity<GameScope> rightCell = null;

                if (tryLeft)
                {
                    leftCell = GetCellOrDefault(side, startIndex - distance);
                    if (leftCell is not null && !leftCell.Has<PlacedCard>())
                        return leftCell;
                }

                if (tryRight)
                {
                    rightCell = GetCellOrDefault(side, startIndex + distance);
                    if (rightCell is not null && !rightCell.Has<PlacedCard>())
                        return rightCell;
                }

                if (leftCell == null && rightCell == null)
                    break;
            }

            return null;
        }

        private static Entity<GameScope> GetCellOrDefault(Side side, int index)
            => Context.GetCellOrDefault(side, index);
    }
}