using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateDeckNeedsShuffleSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .And<DrawingCards>()
                .Build();

        public void Execute()
        {
            foreach (var deck in _decks)
            {
                var cardsLeftInDeck = DeckUtils.GetAllCardsInDeck(deck.ID()).Count;
                deck.Is<NeedsShuffle>(cardsLeftInDeck == 0);
            }
        }
    }
}