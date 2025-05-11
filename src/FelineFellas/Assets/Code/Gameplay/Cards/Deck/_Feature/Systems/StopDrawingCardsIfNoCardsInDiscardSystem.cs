using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StopDrawingCardsIfNoCardsInDiscardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .And<DrawingCards>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInDiscard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InDiscard>()
                .Build();
        private readonly IGroup<Entity<GameScope>> _cardsInDeck
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<CardInDeck>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var deck in _decks.GetEntities(_buffer))
            {
                if (!_cardsInDeck.Any() && !_cardsInDiscard.Any())
                    DeckUtils.StopDrawingCards(deck);
            }
        }
    }
}