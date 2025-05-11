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

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static int HandSize => GameConfig.Cards.HandSize;

        private bool HandIsFull => _cardsInHand.count >= HandSize;

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var deck in _decks)
            {
                deck.Is<DrawingCards>(!HandIsFull);
            }
        }
    }
}