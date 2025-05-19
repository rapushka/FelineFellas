using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class FinishDrawingCardsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .And<DrawingCards>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInHand
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InHandIndex>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<HandSize>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var deck in _decks.GetEntities(_buffer))
            foreach (var actor in _actors)
            {
                var handSize = actor.Get<HandSize>().Value;
                if (_cardsInHand.count >= handSize)
                    DeckUtils.StopDrawingCards(deck);
            }
        }
    }
}