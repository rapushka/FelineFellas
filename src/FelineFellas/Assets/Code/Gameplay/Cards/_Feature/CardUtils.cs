using Entitas.Generic;

namespace FelineFellas
{
    public static class CardUtils
    {
        public static Entity<GameScope> MarkUsedAndDiscard(Entity<GameScope> card)
        {
            return MarkUsed(card)
                    .Is<SendToDiscard>(true)
                ;
        }

        public static Entity<GameScope> MarkUsed(Entity<GameScope> card)
        {
            return RemoveFromHand(card)
                    .Is<Used>(true)
                ;
        }

        public static Entity<GameScope> Discard(Entity<GameScope> card)
        {
            return RemoveFromHand(card)
                    .Is<SendToDiscard>(true)
                ;
        }

        public static Entity<GameScope> RemoveFromHand(Entity<GameScope> card)
        {
            return card
                    .Is<WillBeUsed>(false)
                    .Remove<InHandIndex>()
                    .Is<Interactable>(false)
                ;
        }
    }
}