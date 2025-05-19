using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StopDrawingCardsIfNoCardsInDiscardSystem<TSideCade> : IExecuteSystem
        where TSideCade : FlagComponent, IInScope<GameScope>, new()
    {
        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .And<DrawingCards>()
                .And<TSideCade>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInDiscard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InDiscard>()
                .And<TSideCade>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInDeck
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<CardInDeck>()
                .And<TSideCade>()
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