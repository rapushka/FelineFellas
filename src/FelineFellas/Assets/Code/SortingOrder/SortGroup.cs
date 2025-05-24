using Entitas.Generic;

namespace FelineFellas
{
    public enum SortGroup
    {
        Unknown = 0,

        Grid = 1,

        CardOnField = 2,
        CardInShop = 2,

        CardInDeck = 3,
        CardInDiscard = 3,

        CardInHand = 4,
        DraggingCard = 100,
    }

    public static class SortingOrderExtensions
    {
        public static Entity<GameScope> SetSorting(this Entity<GameScope> @this, SortGroup sortGroup, int additional = 0)
            => @this.Set<SpriteSortingIndex, int>((int)sortGroup + additional);
    }
}