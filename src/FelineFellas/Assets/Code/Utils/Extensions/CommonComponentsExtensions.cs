using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public static class CommonComponentsExtensions
    {
        public static EntityID ID(this Entity<GameScope> @this) => @this.Get<ID>().Value;

        public static Vector2 WorldPosition(this Entity<GameScope> @this)
            => @this.Get<WorldPosition>().Value;

        public static Vector2 WorldPosition(this Entity<InputScope> @this)
            => @this.Get<WorldPosition>().Value;

        public static EntityID GetContainingCellID(this Entity<GameScope> card)
        {
#if DEBUG
            if (!card.Is<OnField>())
                Debug.LogError("Can't Get Cell if Card isn't on Field!");
#endif

            return card.Get<ChildOf>().Value;
        }

        public static (Side side, int index) GetCellCoordinates(this Entity<GameScope> cell)
        {
            var side = cell.Get<OnSide>().Value;
            var index = cell.Get<CellIndex>().Value;

            return (side, index);
        }
    }
}