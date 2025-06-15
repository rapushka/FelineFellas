using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using GameContext = Entitas.Generic.ScopeContext<FelineFellas.GameScope>;
using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public class CellCoordinatesPrimaryIndex
    {
        public const string CellCoordinates = nameof(CellCoordinates);

        private readonly GameContext _context;

        public CellCoordinatesPrimaryIndex(GameContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.AddEntityIndex(
                new PrimaryEntityIndex<GameEntity, Key>(
                    name: CellCoordinates,
                    group: GroupBuilder<GameScope>
                        .With<OnSide>()
                        .And<CellIndex>()
                        .Build(),
                    getKey: GetKey,
                    comparer: new Comparer()
                )
            );
            return;

            Key GetKey(GameEntity entity, IComponent component)
                => new(
                    onSide: (component as OnSide)?.Value ?? entity.Get<OnSide>().Value,
                    cellIndex: (component as CellIndex)?.Value ?? entity.Get<CellIndex>().Value
                );
        }

        public struct Key
        {
            public readonly Side OnSide;
            public readonly int CellIndex;

            public Key(Side onSide, int cellIndex)
            {
                OnSide = onSide;
                CellIndex = cellIndex;
            }
        }

        private class Comparer : IEqualityComparer<Key>
        {
            public bool Equals(Key x, Key y)
                => x.OnSide == y.OnSide
                    && x.CellIndex == y.CellIndex;

            public int GetHashCode(Key obj)
                => HashCode.Combine(obj.OnSide, obj.CellIndex);
        }
    }

    public static class CellCoordinatesPrimaryIndexExtensions
    {
        public static bool TryGetCell(this GameContext context, Side onSide, int cellIndex, out GameEntity cell)
        {
            cell = context.GetCellOrDefault(onSide, cellIndex);
            return cell is not null;
        }

        public static GameEntity GetCell(this GameContext context, Side onSide, int cellIndex)
            => context.GetCellOrDefault(onSide, cellIndex)
                ?? throw new NullReferenceException($"There's no Cell with index {cellIndex} on Side {onSide}");

        public static GameEntity GetCellOrDefault(this GameContext context, Side onSide, int cellIndex)
        {
            var entityIndex = context.GetEntityIndex(CellCoordinatesPrimaryIndex.CellCoordinates);
            var gameIndex = (PrimaryEntityIndex<GameEntity, CellCoordinatesPrimaryIndex.Key>)entityIndex;

            return gameIndex.GetEntity(new(onSide, cellIndex));
        }

        public static GameEntity GetCardOnFieldOrDefault(this GameContext context, Side onSide, int cellIndex)
        {
            var cell = context.GetCellOrDefault(onSide, cellIndex);
            return cell is null
                ? null
                : cell.TryGet<PlacedCard>(out var cardID)
                    ? cardID.Value.GetEntity()
                    : null;
        }
    }
}