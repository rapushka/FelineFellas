using Entitas.Generic;

namespace FelineFellas
{
    public enum RenderOrder
    {
        Unknown = 0,

        Grid = 1,

        CardOnField = 2,
        CardInShop = 2,

        CardInDeck = 3,
        CardInDiscard = 3,

        LeadOnDeck = 4,

        CardInHand = 4,
        DraggingCard = 100,
    }

    public static class SortingOrderExtensions
    {
        public static Entity<GameScope> SetSorting(this Entity<GameScope> @this, RenderOrder renderOrder, int additional = 0)
            => @this.Set<RenderOrderIndex, int>((int)renderOrder + additional);
    }
}