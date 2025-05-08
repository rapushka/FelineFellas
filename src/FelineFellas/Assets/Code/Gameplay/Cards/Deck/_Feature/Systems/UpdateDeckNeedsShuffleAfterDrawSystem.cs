using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateDeckNeedsShuffleAfterDrawSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<DrawCardsEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static int HandSize => GameConfig.Cards.HandSize;

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var deck in _decks)
            {
                var cardsLeftInDeck = DeckUtils.GetAllCardsInDeck(deck.ID()).Count;
                deck.Is<NeedsShuffle>(cardsLeftInDeck < HandSize);
            }
        }
    }
}