using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StartDrawingCardsIfNeedsOnTurnStartedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartTurnEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInHand
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InHandIndex>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _players
            = GroupBuilder<GameScope>
                .With<Player>()
                .And<HandSize>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var deck in _decks)
            foreach (var player in _players)
            {
                var handSize = player.Get<HandSize>().Value;
                var handIsFull = _cardsInHand.count >= handSize;
                deck.Is<DrawingCards>(!handIsFull);
            }
        }
    }
}