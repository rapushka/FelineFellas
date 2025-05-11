using System.Collections.Generic;
using Entitas.Generic;

namespace FelineFellas
{
    public static class DeckUtils
    {
        private static EntityIndex<GameScope, CardInDeck, EntityID> Index
            => Contexts.Instance.Get<GameScope>().GetIndex<CardInDeck, EntityID>();

        public static HashSet<Entity<GameScope>> GetAllCardsInDeck(EntityID deckID)
            => Index.GetEntities(deckID);

        public static void StopDrawingCards(Entity<GameScope> deck)
        {
            deck
                .RemoveSafely<DrawingCards>()
                .RemoveSafely<NeedsShuffle>()
                .RemoveSafely<ShufflingDeckTimer>()
                ;
        }
    }
}