using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ShuffleDeckSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .And<DrawingCards>()
                .And<NeedsShuffle>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _discardedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InDiscard>()
                .Build();

        private readonly List<Entity<GameScope>> _decksBuffer = new(4);
        private readonly List<Entity<GameScope>> _cardsBuffer = new(64);

        public void Execute()
        {
            foreach (var deck in _decks.GetEntities(_decksBuffer))
            foreach (var card in _discardedCards.GetEntities(_cardsBuffer))
            {
                CardUtils.AddToDeck(card, deck);
                deck.Is<NeedsShuffle>(false);
            }
        }
    }
}